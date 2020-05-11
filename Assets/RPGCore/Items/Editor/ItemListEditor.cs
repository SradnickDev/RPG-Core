using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using RPGCore.Database.Item;
using UnityEditor;
using UnityEngine;

namespace RPGCore.Items.Editor
{
	internal class ItemListEditor : IEditorComponent
	{
		public event Action<ItemLabelEditor> SelectionChanged;

		public IEnumerable<ItemDefinition> Items =>
			m_items.Select(item => item.ItemDefinition).ToList();

		public int ItemCount => m_items.Count;
		public ItemLabelEditor Selected;
		private List<ItemLabelEditor> m_items;
		private Vector2 m_scrollPosition;
		private int m_maxItemsPerPage = 10;
		private readonly int[] m_visibleEntries = {10, 20, 40, 60, 80, 100};
		private Event m_event;
		private int m_currentPage = 1;
		private EditorWindow m_window;
		private bool m_isResizing = false;
		private Rect m_changeRect;
		private const float SplitWidth = 5;
		private float m_horizontalSplitterPercent = 0.15f;

		private int MaxPages =>
			Mathf.Max(Mathf.CeilToInt(m_items.Count / (float) m_maxItemsPerPage), 1);

		private bool LeftMousePressed
		{
			get
			{
				if (m_event == null)
				{
					m_event = Event.current;
				}

				return m_event.button == 0 && m_event.isMouse;
			}
		}

		private bool RightMousePressed
		{
			get
			{
				if (m_event == null)
				{
					m_event = Event.current;
				}

				return m_event.type == EventType.ContextClick && m_event.isMouse;
			}
		}

		private float m_scrollViewWidth = 250;

		public ItemListEditor(EditorWindow window)
		{
			m_items = new List<ItemLabelEditor>();
			m_scrollPosition = new Vector2(0, 0);
			m_event = Event.current;

			m_changeRect = new Rect(m_scrollViewWidth,
									window.position.y,
									SplitWidth,
									window.position.height);
			m_window = window;
		}

		public void Add(ItemDefinition itemDefinition)
		{
			m_items.Add(new ItemLabelEditor(itemDefinition));
		}

		public void Set(IEnumerable<ItemDefinition> itemTemplate)
		{
			foreach (var template in itemTemplate)
			{
				m_items.Add(new ItemLabelEditor(template));
			}
		}

		public void Reset()
		{
			m_items = new List<ItemLabelEditor>();
		}

		public void Draw(EditorWindow window)
		{
			GUILayout.BeginVertical(GUILayout.ExpandHeight(true),
									GUILayout.Width(m_scrollViewWidth));
			DrawScrollView(window);
			DrawBottomToolbar();
			GUILayout.EndVertical();
		}

		private void DrawScrollView(EditorWindow window)
		{
			m_scrollPosition = GUILayout.BeginScrollView(m_scrollPosition
													   , false
													   , true
													   , GUILayout.ExpandHeight(true)
													   , GUILayout.Width(m_scrollViewWidth));
			if (m_items.Count == 0)
			{
				GUILayout.Label("No Items available."
							  , EditorStyles.centeredGreyMiniLabel
							  , GUILayout.ExpandWidth(true)
							  , GUILayout.ExpandHeight(true));
			}

			var endIdx = Mathf.Clamp(m_currentPage * m_maxItemsPerPage, 0, m_items.Count);
			var startIdx = Mathf.Clamp(endIdx - m_maxItemsPerPage, 0, m_items.Count);

			for (var i = startIdx; i < endIdx; i++)
			{
				var itemEditorEntry = m_items[i];
				itemEditorEntry.Draw(window);
				UpdateSelection(itemEditorEntry, window);
			}

			GUILayout.EndScrollView();
			HandleHorizontalResize();
		}

		private void DrawBottomToolbar()
		{
			GUILayout.BeginHorizontal(EditorStyles.toolbar, GUILayout.Height(55));

			if (GUILayout.Button(m_maxItemsPerPage + " : Items", EditorStyles.toolbarDropDown))
			{
				var dropdown = new GenericMenu();
				foreach (var viewSize in m_visibleEntries)
				{
					dropdown.AddItem(new GUIContent(viewSize.ToString()),
									 m_maxItemsPerPage == viewSize,
									 () =>
									 {
										 m_maxItemsPerPage = viewSize;
										 if (m_currentPage > MaxPages)
										 {
											 m_currentPage = MaxPages;
										 }
									 });
				}

				dropdown.ShowAsContext();
			}


			if (GUILayout.Button(EditorGUIUtility.IconContent("Profiler.PrevFrame"),
								 EditorStyles.toolbarButton))
			{
				m_currentPage = Mathf.Clamp(m_currentPage -= 1, 1, MaxPages);
			}

			GUILayout.Label($"{m_currentPage}/{MaxPages}");

			if (GUILayout.Button(EditorGUIUtility.IconContent("Profiler.NextFrame"),
								 EditorStyles.toolbarButton))
			{
				m_currentPage = Mathf.Clamp(m_currentPage += 1, 1, MaxPages);
			}


			GUILayout.EndHorizontal();
		}

		private void UpdateSelection(ItemLabelEditor itemLabelEditor, EditorWindow window)
		{
			var rect = GUILayoutUtility.GetLastRect();
			var pos = Event.current.mousePosition;
			var containsRect = rect.Contains(pos);

			if (LeftMousePressed && containsRect)
			{
				OnSelectionChanged(itemLabelEditor, window);
			}

			if (RightMousePressed && containsRect)
			{
				ContextMenu(itemLabelEditor);
			}
		}

		private void ContextMenu(ItemLabelEditor itemLabelEditor)
		{
			GUI.FocusControl(null);
			var targetLabel = itemLabelEditor;

			void Save() => ItemDatabase.Update(targetLabel.ItemDefinition);

			void Reset()
			{
				var origin = ItemDatabase.Fetch(targetLabel.ItemDefinition.Id);
				targetLabel.Set(origin);
				SelectionChanged?.Invoke(targetLabel);
			}

			void Delete()
			{
				ItemDatabase.DeleteById(targetLabel.ItemDefinition.Id);
				m_items.Remove(targetLabel);
				SelectionChanged?.Invoke(null);
			}

			void Duplicate()
			{
				for (int i = 0; i < 1000; i++)
				{
					var copy =
						(ItemDefinition) Activator.CreateInstance(targetLabel.ItemDefinition.GetType(),
																  targetLabel.ItemDefinition);
					copy.Id = ObjectId.GenerateNewId();
					copy.DisplayName = "copy_" + copy.DisplayName;
					var newLabel = new ItemLabelEditor(copy);
					m_items.Add(newLabel);

					OnSelectionChanged(newLabel, m_window);
				}
			}

			var menu = new GenericMenu();
			menu.AddDisabledItem(new GUIContent(targetLabel.ItemName, "Selected Item"));
			menu.AddItem(new GUIContent("Save", "Saved to Database."), false, Save);
			menu.AddItem(new GUIContent("Reset", "Reset to Origin."), false, Reset);
			menu.AddItem(new GUIContent("Duplicate", "Creates a Copy.Not saved to Database."),
						 false,
						 Duplicate);
			menu.AddItem(new GUIContent("Delete", "Delete from Database"), false, Delete);
			menu.ShowAsContext();
			m_event.Use();
		}

		private void OnSelectionChanged(ItemLabelEditor newSelection, EditorWindow window)
		{
			if (newSelection != Selected)
			{
				Selected?.Deselect();
				Selected = newSelection;
				newSelection.Select();

				window.Repaint();
				SelectionChanged?.Invoke(Selected);
			}
		}

		private void HandleHorizontalResize()
		{
			EditorGUIUtility.AddCursorRect(m_changeRect, MouseCursor.ResizeHorizontal);

			if (Event.current.type == EventType.MouseDown
			 && m_changeRect.Contains(Event.current.mousePosition))
			{
				m_isResizing = true;
			}


			if (m_isResizing)
			{
				m_horizontalSplitterPercent =
					Mathf.Clamp(Event.current.mousePosition.x / m_window.position.width, 0.15f,
								0.5f);
				m_changeRect.x = (int) (m_window.position.width * m_horizontalSplitterPercent);
				m_scrollViewWidth = m_changeRect.x;

				m_window.Repaint();
			}

			if (Event.current.type == EventType.MouseUp)
			{
				m_isResizing = false;
			}
		}
	}
}
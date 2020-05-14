using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using RPGCore.Database.Item;
using RPGCore.Editor;
using UnityEditor;
using UnityEngine;

namespace RPGCore.Items.Editor
{
	internal class ItemListEditor : ResizeableEditor
	{
		public event Action<ItemEntryEditor> SelectionChanged;

		public IEnumerable<ItemDefinition> Items =>
			m_items.Select(item => item.ItemDefinition).ToList();

		public int ItemCount => m_items.Count;
		public ItemEntryEditor Selected;
		private List<ItemEntryEditor> m_items;
		private Vector2 m_scrollPosition;
		private int m_maxItemsPerPage = 60;
		private readonly int[] m_visibleEntries = {10, 20, 40, 60, 80, 100};
		private Event m_event;
		private int m_currentPage = 1;

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

		public ItemListEditor()
		{
			m_items = new List<ItemEntryEditor>();
			m_scrollPosition = new Vector2(0, 0);
			m_event = Event.current;
		}

		public void Add(ItemDefinition itemDefinition)
		{
			var newLabel = new ItemEntryEditor(itemDefinition);
			newLabel.OnEnable();
			m_items.Add(newLabel);
		}

		public void Set(IEnumerable<ItemDefinition> itemTemplate)
		{
			foreach (var template in itemTemplate)
			{
				m_items.Add(new ItemEntryEditor(template));
			}
		}

		public void Reset()
		{
			m_items = new List<ItemEntryEditor>();
		}

		public override void Draw()
		{
			GUILayout.BeginVertical(GUILayout.ExpandHeight(true),
									GUILayout.Width(ListWidth));
			DrawScrollView(Window);
			DrawBottomToolbar();
			GUILayout.EndVertical();
		}

		private void DrawScrollView(EditorWindow window)
		{
			m_scrollPosition = GUILayout.BeginScrollView(m_scrollPosition
													   , false
													   , true
													   , GUILayout.ExpandHeight(true)
													   , GUILayout.Width(ListWidth));
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
				itemEditorEntry.Draw();
				UpdateSelection(itemEditorEntry, window);
			}

			GUILayout.EndScrollView();
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
			HandleHorizontalResize();
		}

		private void UpdateSelection(ItemEntryEditor itemEntryEditor, EditorWindow window)
		{
			var rect = GUILayoutUtility.GetLastRect();
			var pos = Event.current.mousePosition;
			var containsRect = rect.Contains(pos);

			if (LeftMousePressed && containsRect)
			{
				OnSelectionChanged(itemEntryEditor);
			}

			if (RightMousePressed && containsRect)
			{
				ContextMenu(itemEntryEditor);
			}
		}

		private void ContextMenu(ItemEntryEditor itemEntryEditor)
		{
			GUI.FocusControl(null);
			var targetLabel = itemEntryEditor;

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
				//TODO DELETE DEBUG CODE
				for (int i = 0; i < 1000; i++)
				{
					var copy =
						(ItemDefinition)
						Activator.CreateInstance(targetLabel.ItemDefinition.GetType(),
												 targetLabel.ItemDefinition);
					copy.Id = ObjectId.GenerateNewId();
					copy.DisplayName = "copy_" + copy.DisplayName;
					var newLabel = new ItemEntryEditor(copy);
					m_items.Add(newLabel);

					OnSelectionChanged(newLabel);
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

		private void OnSelectionChanged(ItemEntryEditor newSelection)
		{
			if (newSelection != Selected)
			{
				Selected?.Deselect();
				Selected = newSelection;
				newSelection.Select();

				Window.Repaint();
				SelectionChanged?.Invoke(Selected);
			}
		}
	}
}
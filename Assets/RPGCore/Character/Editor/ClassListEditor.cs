using System;
using System.Collections.Generic;
using RPGCore.Editor;
using UnityEditor;
using UnityEngine;

namespace RPGCore.Character.Editor
{
	internal class ClassListEditor : ResizeableEditor
	{
		public event Action<ClassListEntry> SelectionChanged;
		public ClassListEntry Selected;
		private List<ClassListEntry> m_class;
		private Vector2 m_scrollPosition;
		private Event m_event;

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

		public ClassListEditor()
		{
			m_class = new List<ClassListEntry>();
			m_scrollPosition = new Vector2(0, 0);
			m_event = Event.current;
		}

		public void Add(CharacterClass charClass)
		{
			var newLabel = new ClassListEntry(charClass);
			newLabel.OnEnable();
			m_class.Add(newLabel);
		}

		public override void Draw()
		{
			DrawScrollView(Window);
		}

		private void DrawScrollView(EditorWindow window)
		{
			m_scrollPosition = GUILayout.BeginScrollView(m_scrollPosition
													   , false
													   , true
													   , GUILayout.ExpandHeight(true)
													   , GUILayout.Width(ListWidth));
			if (m_class.Count == 0)
			{
				GUILayout.Label("No Items available."
							  , EditorStyles.centeredGreyMiniLabel
							  , GUILayout.ExpandWidth(true)
							  , GUILayout.ExpandHeight(true));
			}

			foreach (var label in m_class)
			{
				label.Draw();
				UpdateSelection(label, window);
			}


			GUILayout.EndScrollView();
		}

		private void UpdateSelection(ClassListEntry listEntry, EditorWindow window)
		{
			var rect = GUILayoutUtility.GetLastRect();
			var pos = Event.current.mousePosition;
			var containsRect = rect.Contains(pos);

			if (LeftMousePressed && containsRect)
			{
				OnSelectionChanged(listEntry);
			}
		}

		private void OnSelectionChanged(ClassListEntry newSelection)
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
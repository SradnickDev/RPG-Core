using System.Collections.Generic;
using RPGCore.Items;
using RPGCore.Items.Editor;
using UnityEditor;
using UnityEngine;

namespace RPGCore.Character.Editor
{
	internal class ClassBodyEditor : IEditorComponent
	{
		public EditorWindow Window
		{
			get => m_window;
			set
			{
				m_window = value;
				foreach (var cp in m_components.Values)
				{
					cp.Window = m_window;
				}
			}
		}

		private EditorWindow m_window;

		private ClassInfoDrawer m_classInfoDrawer;

		private CharacterClass m_charClass;
		private bool m_draw = false;
		private int m_currentTab = 0;
		private string[] m_tabs = new[] {"LvL & Stats", "Behaviour", "Other"};
		private Dictionary<int, BodyDrawer<ItemDefinition>> m_components;

		public ClassBodyEditor()
		{
			m_classInfoDrawer = new ClassInfoDrawer();

			m_components = new Dictionary<int, BodyDrawer<ItemDefinition>>()
			{
				{0, new StatModifierDrawer()},
				{1, new BehaviourDrawer()},
				{2, new OtherDrawer()},
			};
		}

		public void Set(CharacterClass charClass)
		{
			if (charClass == null)
			{
				m_draw = false;
				m_charClass = null;
			}

			m_classInfoDrawer.Source = charClass;
			foreach (var cp in m_components.Values)
			{
				//cp.CharacterClass = charClass;
			}

			m_charClass = charClass;
			EditorGUI.FocusTextInControl("");
			Window.Repaint();
		}

		public void OnEnable()
		{
			foreach (var cp in m_components.Values)
			{
				cp.OnEnable();
			}
		}

		public void Draw()
		{
			GUILayout.BeginHorizontal(GUILayout.ExpandWidth(true)
									, GUILayout.ExpandHeight(true));

			if (Event.current.type == EventType.Layout && m_charClass != null)
			{
				m_draw = true;
			}


			DrawBody();

			GUILayout.EndHorizontal();
		}

		private void DrawBody()
		{
			if (!m_draw)
			{
				DrawEmptySelectionLabel();
			}
			else
			{
				GUILayout.BeginVertical();
				m_classInfoDrawer.Draw();
				DrawOptions();
				GUILayout.EndVertical();
			}
		}

		private void DrawOptions()
		{
			m_currentTab = GUILayout.Toolbar(m_currentTab, m_tabs);
			GUILayout.BeginVertical();

			m_components[m_currentTab].Draw();


			GUILayout.EndVertical();
		}

		private void DrawEmptySelectionLabel()
		{
			GUILayout.Label("No Item selected."
						  , EditorStyles.centeredGreyMiniLabel
						  , GUILayout.ExpandWidth(true)
						  , GUILayout.ExpandHeight(true));
		}

		public void OnDisable()
		{
			foreach (var cp in m_components.Values)
			{
				cp.OnDisable();
			}
		}
	}
}
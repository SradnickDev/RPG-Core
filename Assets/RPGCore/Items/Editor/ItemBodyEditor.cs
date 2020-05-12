using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace RPGCore.Items.Editor
{
	internal class ItemBodyEditor : IEditorComponent
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

		private ItemInfoDrawer m_itemInfoDrawer;

		private ItemDefinition m_itemDefinition;
		private bool m_draw = false;
		private int m_currentTab = 0;
		private string[] m_tabs = new[] {"Stats", "Behaviour", "Other"};
		private Dictionary<int, ItemBodyDrawer> m_components;

		public ItemBodyEditor()
		{
			m_itemInfoDrawer = new ItemInfoDrawer();

			m_components = new Dictionary<int, ItemBodyDrawer>()
			{
				{0, new ItemStatModifierDrawer()},
				{1, new ItemBehaviourDrawer()},
				{2, new ItemOtherDrawer()},
			};
		}

		public void Set(ItemDefinition definition)
		{
			if (definition == null)
			{
				m_draw = false;
				m_itemDefinition = null;
			}

			m_itemInfoDrawer.Definition = definition;
			foreach (var cp in m_components.Values)
			{
				cp.Definition = definition;
			}

			m_itemDefinition = definition;
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

			if (Event.current.type == EventType.Layout && m_itemDefinition != null)
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
				m_itemInfoDrawer.Draw();
				DrawItemOptions();
				GUILayout.EndVertical();
			}
		}

		private void DrawItemOptions()
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
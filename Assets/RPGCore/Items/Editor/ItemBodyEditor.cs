using UnityEditor;
using UnityEngine;

namespace RPGCore.Items.Editor
{
	internal class ItemBodyEditor : IEditorComponent
	{
		private ItemInfoDrawer m_itemInfoDrawer;
		private ItemStatsDrawer m_itemStatsDrawer;
		private ItemBehaviourDrawer m_itemBehaviourDrawer;
		private ItemOtherDrawer m_itemOtherDrawer;

		private ItemDefinition m_itemDefinition;
		private EditorWindow m_editorWindow;
		private bool m_draw = false;
		private int m_currentTab = 0;
		private string[] m_tabs = new[] {"Stats", "Behaviour", "Other"};

		public ItemBodyEditor()
		{
			m_itemInfoDrawer = new ItemInfoDrawer();
			m_itemStatsDrawer = new ItemStatsDrawer();
			m_itemBehaviourDrawer = new ItemBehaviourDrawer();
			m_itemOtherDrawer = new ItemOtherDrawer();
		}

		public void Set(ItemDefinition definition)
		{
			if (definition == null)
			{
				m_draw = false;
				m_itemDefinition = null;
			}

			m_itemInfoDrawer.Definition = definition;
			m_itemStatsDrawer.Definition = definition;
			m_itemBehaviourDrawer.Definition = definition;
			m_itemOtherDrawer.Definition = definition;

			m_itemDefinition = definition;
			EditorGUI.FocusTextInControl("");
			m_editorWindow.Repaint();
		}

		public void Draw(EditorWindow editorWindow)
		{
			m_editorWindow = editorWindow;

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
				m_itemInfoDrawer.Draw(m_editorWindow);
				DrawItemOptions();
				GUILayout.EndVertical();
			}
		}

		private void DrawItemOptions()
		{
			m_currentTab = GUILayout.Toolbar(m_currentTab, m_tabs);
			GUILayout.BeginVertical();

			switch (m_currentTab)
			{
				case 0:
					m_itemStatsDrawer.Draw(m_editorWindow);
					break;
				case 1:
					m_itemBehaviourDrawer.Draw(m_editorWindow);
					break;
				case 2:
					m_itemOtherDrawer.Draw(m_editorWindow);
					break;
			}

			GUILayout.EndVertical();
		}

		private void DrawEmptySelectionLabel()
		{
			GUILayout.Label("No Item selected."
						  , EditorStyles.centeredGreyMiniLabel
						  , GUILayout.ExpandWidth(true)
						  , GUILayout.ExpandHeight(true));
		}
	}
}
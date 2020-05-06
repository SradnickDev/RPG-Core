using RPGCore.Items.Types.Templates;
using RPGCore.Stat;
using RPGCore.Utilities;
using UnityEditor;
using UnityEngine;

namespace RPGCore.Items.Editor
{
	internal class ItemBodyEditor : IEditorComponent
	{
		private ItemTemplate m_itemTemplate;
		private EditorWindow m_editorWindow;
		private bool m_draw = false;
		private int m_currentTab = 0;
		private string[] m_tabs = new[] {"Stats", "Behaviour", "Other"};

		public void Set(ItemTemplate itemEditorLabel)
		{
			if (itemEditorLabel == null)
			{
				m_draw = false;
				m_itemTemplate = null;
			}

			m_itemTemplate = itemEditorLabel;
			EditorGUI.FocusTextInControl("");
			m_editorWindow.Repaint();
		}

		public void Draw(EditorWindow editorWindow)
		{
			m_editorWindow = editorWindow;

			GUILayout.BeginHorizontal(GUILayout.ExpandWidth(true)
									, GUILayout.ExpandHeight(true));

			if (Event.current.type == EventType.Layout && m_itemTemplate != null)
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
				DrawItemInfo();
				DrawItemOptions();
				GUILayout.EndVertical();
			}
		}

		private void DrawItemInfo()
		{
			GUILayout.BeginVertical(EditorStyles.helpBox, GUILayout.MaxWidth(450));

			GUILayout.BeginHorizontal();
			var labelStyle = new GUIStyle(GUI.skin.label);
			labelStyle.alignment = TextAnchor.MiddleLeft;
			labelStyle.normal.textColor = Color.grey;
			labelStyle.fontSize = 9;

			GUILayout.Label($"{m_itemTemplate.ReadableType()}", labelStyle);
			GUILayout.Label("Item ID :" + m_itemTemplate.Id, labelStyle);
			GUILayout.FlexibleSpace();
			GUILayout.EndHorizontal();

			GUILayout.Label("Item Info", EditorStyles.boldLabel);
			m_itemTemplate.DisplayName =
				EditorGUILayout.TextField("Display Name", m_itemTemplate.DisplayName);

			GUILayout.BeginHorizontal();
			GUILayout.Label("Description", GUILayout.Width(146));
			m_itemTemplate.Description =
				EditorGUILayout.TextArea(m_itemTemplate.Description, GUILayout.MaxWidth(450));

			GUILayout.EndHorizontal();

			m_itemTemplate.Rarity =
				(Rarity) EditorGUILayout.EnumPopup("Rarity ", m_itemTemplate.Rarity);

			GUILayout.BeginHorizontal();
			GUILayout.Label("Icon", GUILayout.Width(146));
			m_itemTemplate.Icon.Data =
				(Sprite) EditorGUILayout.ObjectField(m_itemTemplate.Icon.Data, typeof(Sprite), false,
													 GUILayout.Width(65), GUILayout.Height(65));
			GUILayout.EndHorizontal();

			GUILayout.EndVertical();
		}

		private void DrawItemOptions()
		{
			m_currentTab = GUILayout.Toolbar(m_currentTab, m_tabs);
			GUILayout.BeginVertical();

			switch (m_currentTab)
			{
				case 0:
					DrawStats();
					break;
				case 1:
					DrawBehaviour();
					break;
				case 2:
					DrawOther();
					break;
			}

			GUILayout.EndVertical();
		}

		private void DrawStats()
		{
			GUILayout.Space(20);

			GUILayout.BeginVertical();
			StatCollection stats = null;
			if (m_itemTemplate.GetType() == typeof(ArmorItemTemplate))
			{
				stats = ((ArmorItemTemplate) m_itemTemplate).Stats;
			}

			if (m_itemTemplate.GetType() == typeof(WeaponItemTemplate))
			{
				stats = ((WeaponItemTemplate) m_itemTemplate).Stats;
			}

			for (var i = 0; i < stats.Count; i++)
			{
				var stat = stats[i];
				EditorExtension.DrawStat(stat, s => stats.Remove(s));
			}

			GUILayout.EndVertical();

			GUILayout.FlexibleSpace();
			GUILayout.BeginHorizontal(EditorStyles.toolbar,
									  GUILayout.ExpandWidth(true),
									  GUILayout.MaxHeight(22));


			var addIcon = EditorGUIUtility.IconContent("Toolbar Plus More");
			EditorExtension.ClassDropDown<BaseStat>(new GUIContent(addIcon)
												  , t =>
													{
														t.BaseValue = 20;
														stats?.Add(t);
													}, EditorStyles.toolbarButton);

			if (GUILayout.Button(EditorGUIUtility.IconContent("TreeEditor.Trash"),
								 EditorStyles.toolbarButton))
			{
				stats.Clear();
			}

			GUILayout.EndHorizontal();
		}

		private void DrawBehaviour()
		{
			Debug.Log("be");
		}

		private void DrawOther()
		{
			Debug.Log("ot");
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
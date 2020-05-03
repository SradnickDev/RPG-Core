using UnityEditor;
using UnityEngine;

namespace RPGCore.Items.Editor
{
	internal class ItemBodyEditor : IEditorComponent
	{
		private ItemTemplate m_itemTemplate;
		private EditorWindow m_editorWindow;
		private bool m_draw = false;

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

			GUILayout.BeginHorizontal(EditorStyles.helpBox
									, GUILayout.ExpandWidth(true)
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

			GUILayout.Label($"{m_itemTemplate.Name()}", labelStyle);
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
			m_itemTemplate.Icon =
				(Sprite) EditorGUILayout.ObjectField(m_itemTemplate.Icon, typeof(Sprite), false,
													 GUILayout.Width(65), GUILayout.Height(65));
			GUILayout.EndHorizontal();

			GUILayout.EndVertical();
		}

		private void DrawItemOptions()
		{
			GUILayout.BeginVertical(EditorStyles.helpBox, GUILayout.ExpandHeight(true),
									GUILayout.ExpandWidth(true));

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
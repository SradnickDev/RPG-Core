using RPGCore.Utilities;
using UnityEditor;
using UnityEngine;

namespace RPGCore.Items.Editor
{
	public class ItemLabelEditor : IEditorComponent
	{
		public string ItemName => m_itemTemplate.DisplayName;
		public ItemTemplate ItemTemplate => m_itemTemplate;

		private ItemTemplate m_itemTemplate;
		private Vector2 m_iconSize = new Vector2(30, 30);
		private GUIStyle m_style;
		private GUIStyle SelectedStyle => new GUIStyle("MeTransitionSelectHead");
		private GUIStyle DeselectedStyle => new GUIStyle("PreferencesSectionBox");

		public ItemLabelEditor(ItemTemplate itemTemplate)
		{
			m_itemTemplate = itemTemplate;
			m_style = DeselectedStyle;
		}

		public void Set(ItemTemplate itemTemplate)
		{
			m_itemTemplate = itemTemplate;
		}

		public void Draw(EditorWindow window)
		{
			if (m_itemTemplate == null)
			{
				Debug.LogError("Item Editor entry, ItemTemplate is null!");
				return;
			}

			DrawLabel();
		}

		private void DrawLabel()
		{
			GUILayout.BeginHorizontal(m_style
									, GUILayout.ExpandWidth(true)
									, GUILayout.MaxHeight(30)
									, GUILayout.Height(30));
			{
				var icon = m_itemTemplate.Icon == null
					? EditorGUIUtility.FindTexture("BuildSettings.Broadcom")
					: m_itemTemplate.Icon.texture;

				GUILayout.Label(icon, GUILayout.Height(m_iconSize.x), GUILayout.Width(m_iconSize.y),
								GUILayout.MaxHeight(30));

				GUILayout.BeginVertical();
				{
					var labelStyle = new GUIStyle(GUI.skin.label);
					labelStyle.alignment = TextAnchor.MiddleLeft;
					labelStyle.normal.textColor = m_itemTemplate.ItemColor();
					GUILayout.Label(ItemName, labelStyle);


					labelStyle.normal.textColor = Color.black;
					labelStyle.fontSize = 9;
					GUILayout.Label(m_itemTemplate.ReadableType(), labelStyle);
				}
				GUILayout.EndVertical();
			}
			GUILayout.EndHorizontal();
		}

		public void Select()
		{
			m_style = SelectedStyle;
		}

		public void Deselect()
		{
			m_style = DeselectedStyle;
		}
	}
}
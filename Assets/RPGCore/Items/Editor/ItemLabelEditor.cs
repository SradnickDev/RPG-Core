using RPGCore.Utilities;
using UnityEditor;
using UnityEngine;

namespace RPGCore.Items.Editor
{
	public class ItemLabelEditor : IEditorComponent
	{
		public EditorWindow Window { get; set; }
		public string ItemName => m_itemDefinition.DisplayName;
		public ItemDefinition ItemDefinition => m_itemDefinition;

		private ItemDefinition m_itemDefinition;
		private Vector2 m_iconSize = new Vector2(30, 30);
		private GUIStyle m_style;
		private GUIStyle SelectedStyle => new GUIStyle("MeTransitionSelectHead");
		private GUIStyle DeselectedStyle => new GUIStyle("PreferencesSectionBox");

		public ItemLabelEditor(ItemDefinition itemDefinition)
		{
			m_itemDefinition = itemDefinition;
			m_style = DeselectedStyle;
		}

		public void Set(ItemDefinition itemDefinition)
		{
			m_itemDefinition = itemDefinition;
		}

		public void OnEnable() { }

		public void Draw()
		{
			if (m_itemDefinition == null)
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
				var icon = m_itemDefinition.Icon.Data == null
					? EditorGUIUtility.FindTexture("BuildSettings.Broadcom")
					: m_itemDefinition.Icon.Data.texture;

				GUILayout.Label(icon, GUILayout.Height(m_iconSize.x), GUILayout.Width(m_iconSize.y),
								GUILayout.MaxHeight(30));

				GUILayout.BeginVertical();
				{
					var labelStyle = new GUIStyle(GUI.skin.label);
					labelStyle.alignment = TextAnchor.MiddleLeft;
					labelStyle.normal.textColor = m_itemDefinition.ItemColor();
					GUILayout.Label(ItemName, labelStyle);


					labelStyle.normal.textColor = Color.black;
					labelStyle.fontSize = 9;
					GUILayout.Label(m_itemDefinition.ReadableType(), labelStyle);
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

		public void OnDisable() { }
	}
}
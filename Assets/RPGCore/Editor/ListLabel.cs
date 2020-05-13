using RPGCore.Items.Editor;
using UnityEditor;
using UnityEngine;

namespace RPGCore.Editor
{
	public abstract class ListLabel : IEditorComponent
	{
		public EditorWindow Window { get; set; }
		private GUIStyle SelectedStyle => new GUIStyle("MeTransitionSelectHead");
		private GUIStyle DeselectedStyle => new GUIStyle("PreferencesSectionBox");

		private Vector2 m_iconSize = new Vector2(30, 30);
		private GUIStyle m_style;
		private Texture2D m_icon;
		private string m_text;
		private string m_info;
		private Color m_labelColor;

		protected ListLabel()
		{
			m_style = DeselectedStyle;
		}

		public void OnEnable() { }

		public void Set(string name, string labelInfo, Texture2D icon, Color labelColor)
		{
			m_text = name;
			m_icon = icon;
			m_info = labelInfo;
			m_labelColor = labelColor;
		}

		public virtual void Draw() => DrawLabel();

		private void DrawLabel()
		{
			GUILayout.BeginHorizontal(m_style
									, GUILayout.ExpandWidth(true)
									, GUILayout.MaxHeight(30)
									, GUILayout.Height(30));
			{
				var icon = m_icon == null
					? EditorGUIUtility.FindTexture("BuildSettings.Broadcom")
					: m_icon;

				GUILayout.Label(icon, GUILayout.Height(m_iconSize.x), GUILayout.Width(m_iconSize.y),
								GUILayout.MaxHeight(30));

				GUILayout.BeginVertical();
				{
					var labelStyle = new GUIStyle(GUI.skin.label);
					labelStyle.alignment = TextAnchor.MiddleLeft;
					labelStyle.normal.textColor = m_labelColor;
					GUILayout.Label(m_text, labelStyle);


					labelStyle.normal.textColor = Color.black;
					labelStyle.fontSize = 9;
					GUILayout.Label(m_info, labelStyle);
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
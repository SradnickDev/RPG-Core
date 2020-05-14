using UnityEditor;
using UnityEngine;

namespace RPGCore.Items.Editor
{
	public static class RpgEditorStyles
	{
		private static GUIStyle m_leftMiniLabel,
								m_centeredLabel,
								m_selectedLabel,
								m_deselectedLabel,
								m_centeredGreyMiniLabel,
								m_titleStyle;

		public static GUIStyle LeftMiniLabel
		{
			get
			{
				if (m_leftMiniLabel == null)
				{
					m_leftMiniLabel =
						new GUIStyle(GUI.skin.label)
							.Alignment(TextAnchor.MiddleLeft)
							.NormalTextColor(Color.black)
							.FontSize(9);
				}

				return m_leftMiniLabel;
			}
		}

		public static GUIStyle CenteredLeftLabel
		{
			get
			{
				if (m_centeredLabel == null)
				{
					m_centeredLabel = new GUIStyle(GUI.skin.label)
					{
						alignment = TextAnchor.MiddleLeft,
						normal = {textColor = Color.black},
					};
				}

				return m_centeredLabel;
			}
		}

		public static GUIStyle SelectedLabel
		{
			get
			{
				if (m_selectedLabel == null)
				{
					m_selectedLabel = new GUIStyle("MeTransitionSelectHead");
				}

				return m_selectedLabel;
			}
		}

		public static GUIStyle DeselectedLabel
		{
			get
			{
				if (m_deselectedLabel == null)
				{
					m_deselectedLabel = new GUIStyle("PreferencesSectionBox");
				}

				return m_deselectedLabel;
			}
		}

		public static GUIStyle LeftGreyMiniLabel
		{
			get
			{
				if (m_centeredGreyMiniLabel == null)
				{
					m_centeredGreyMiniLabel =
						new GUIStyle(GUI.skin.label)
							.Alignment(TextAnchor.MiddleCenter)
							.FontSize(9);
				}

				return m_leftMiniLabel;
			}
		}

		public static GUIStyle TitleStyle
		{
			get
			{
				if (m_titleStyle == null)
				{
					m_titleStyle =
						new GUIStyle(GUI.skin.label)
							.FontStyle(FontStyle.Bold)
							.RichText(true)
							.Alignment(TextAnchor.UpperLeft)
							.NormalTextColor(Color.black)
							.FontSize(12);
				}

				return m_titleStyle;
			}
		}

		public static void SelectableListEntry(GUIStyle style,
											   Texture2D labelIcon,
											   string name,
											   Color labelColor,
											   string info)
		{
			GUILayout.BeginHorizontal(style
									, GUILayout.ExpandWidth(true)
									, GUILayout.MaxHeight(30)
									, GUILayout.Height(30));
			{
				var icon = labelIcon == null
					? EditorGUIUtility.FindTexture("BuildSettings.Broadcom")
					: labelIcon;

				GUILayout.Label(icon, GUILayout.Height(30), GUILayout.Width(30),
								GUILayout.MaxHeight(30));

				GUILayout.BeginVertical();
				{
					GUILayout.Label(name,
									RpgEditorStyles.CenteredLeftLabel.NormalTextColor(labelColor));

					GUILayout.Label(info, RpgEditorStyles.LeftMiniLabel);
				}
				GUILayout.EndVertical();
			}
			GUILayout.EndHorizontal();
		}
	}
}
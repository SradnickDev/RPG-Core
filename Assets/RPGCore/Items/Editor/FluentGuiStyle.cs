using UnityEngine;

namespace RPGCore.Items.Editor
{
	public static class FluentGuiStyle
	{
		public static GUIStyle NormalTextColor(this GUIStyle style, Color color)
		{
			style.normal.textColor = color;
			return style;
		}

		public static GUIStyle NormalBackground(this GUIStyle style, Texture2D texture)
		{
			style.normal.background = texture;
			return style;
		}

		public static GUIStyle Alignment(this GUIStyle style, TextAnchor anchor)
		{
			style.alignment = anchor;
			return style;
		}

		public static GUIStyle FontSize(this GUIStyle style, int size)
		{
			style.fontSize = size;
			return style;
		}

		public static GUIStyle FontStyle(this GUIStyle style, FontStyle fontStyle)
		{
			style.fontStyle = fontStyle;
			return style;
		}

		public static GUIStyle RichText(this GUIStyle style, bool value)
		{
			style.richText = value;
			return style;
		}
	}
}
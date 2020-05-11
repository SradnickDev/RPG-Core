using RPGCore.Utilities;
using UnityEditor;
using UnityEngine;

namespace RPGCore.Items.Editor
{
	internal class ItemInfoDrawer : ItemBodyDrawer
	{
		public override void Draw(EditorWindow editorWindow)
		{
			DrawItemInfo();
		}

		private void DrawItemInfo()
		{
			GUILayout.BeginVertical(EditorStyles.helpBox, GUILayout.MaxWidth(450));

			GUILayout.BeginHorizontal();
			var labelStyle = new GUIStyle(GUI.skin.label);
			labelStyle.alignment = TextAnchor.MiddleLeft;
			labelStyle.normal.textColor = Color.grey;
			labelStyle.fontSize = 9;

			GUILayout.Label($"{Definition.ReadableType()}", labelStyle);
			GUILayout.Label("Item ID :" + Definition.Id, labelStyle);
			GUILayout.FlexibleSpace();
			GUILayout.EndHorizontal();

			GUILayout.Label("Item Info", EditorStyles.boldLabel);
			Definition.DisplayName =
				EditorGUILayout.TextField("Display Name", Definition.DisplayName);

			GUILayout.BeginHorizontal();
			GUILayout.Label("Description", GUILayout.Width(146));
			Definition.Description =
				EditorGUILayout.TextArea(Definition.Description, GUILayout.MaxWidth(450));

			GUILayout.EndHorizontal();

			Definition.Rarity =
				(Rarity) EditorGUILayout.EnumPopup("Rarity ", Definition.Rarity);

			GUILayout.BeginHorizontal();
			GUILayout.Label("Icon", GUILayout.Width(146));
			Definition.Icon.Data =
				(Sprite) EditorGUILayout.ObjectField(Definition.Icon.Data, typeof(Sprite),
													 false,
													 GUILayout.Width(65), GUILayout.Height(65));
			GUILayout.EndHorizontal();

			GUILayout.EndVertical();
		}
	}
}
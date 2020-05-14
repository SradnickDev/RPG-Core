using RPGCore.Utilities;
using UnityEditor;
using UnityEngine;

namespace RPGCore.Items.Editor
{
	internal class ItemInfoEditorComponent : EditorComponent<ItemDefinition>
	{
		public override void Draw()
		{
			DrawItemInfo();
		}

		private void DrawItemInfo()
		{
			GUILayout.BeginVertical(EditorStyles.helpBox, GUILayout.MaxWidth(450));

			GUILayout.BeginHorizontal();

			GUILayout.Label($"{Source.ReadableType()}", RpgEditorStyles.LeftGreyMiniLabel);
			GUILayout.Label("Item ID :" + Source.Id, RpgEditorStyles.LeftGreyMiniLabel);
			GUILayout.FlexibleSpace();
			GUILayout.EndHorizontal();

			GUILayout.Label("Item Info", EditorStyles.boldLabel);
			Source.DisplayName =
				EditorGUILayout.TextField("Display Name", Source.DisplayName);

			GUILayout.BeginHorizontal();
			GUILayout.Label("Description", GUILayout.Width(146));
			Source.Description =
				EditorGUILayout.TextArea(Source.Description, GUILayout.MaxWidth(450));

			GUILayout.EndHorizontal();

			Source.Rarity =
				(Rarity) EditorGUILayout.EnumPopup("Rarity ", Source.Rarity);

			GUILayout.BeginHorizontal();
			GUILayout.Label("Icon", GUILayout.Width(146));
			Source.Icon.Data =
				(Sprite) EditorGUILayout.ObjectField(Source.Icon.Data, typeof(Sprite),
													 false,
													 GUILayout.Width(65), GUILayout.Height(65));
			GUILayout.EndHorizontal();

			GUILayout.EndVertical();
		}
	}
}
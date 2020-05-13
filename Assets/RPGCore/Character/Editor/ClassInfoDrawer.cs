using RPGCore.Items.Editor;
using UnityEditor;
using UnityEngine;

namespace RPGCore.Character.Editor
{
	internal class ClassInfoDrawer : BodyDrawer<CharacterClass>
	{
		public override void Draw()
		{
			DrawItemInfo();
		}

		private void DrawItemInfo()
		{
			GUILayout.BeginVertical(EditorStyles.helpBox, GUILayout.MaxWidth(450));


			GUILayout.Label("Class Info", EditorStyles.boldLabel);

			GUILayout.BeginHorizontal();
			GUILayout.Label("Description", GUILayout.Width(146));
			Source.Description =
				EditorGUILayout.TextArea(Source.Description, GUILayout.MaxWidth(450));

			GUILayout.EndHorizontal();

			GUILayout.BeginHorizontal();
			GUILayout.Label("Icon", GUILayout.Width(146));
			Source.Icon =
				(Sprite) EditorGUILayout.ObjectField(Source.Icon, typeof(Sprite),
													 false,
													 GUILayout.Width(65), GUILayout.Height(65));
			GUILayout.EndHorizontal();

			GUILayout.EndVertical();
		}
	}
}
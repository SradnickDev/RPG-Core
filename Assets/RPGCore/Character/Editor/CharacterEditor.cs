using RPGCore.Items.Editor;
using UnityEditor;
using UnityEngine;

namespace RPGCore.Character.Editor
{
	internal class CharacterEditor : IEditorComponent
	{
		public EditorWindow Window { get; set; }
		public void OnEnable() { }

		public void Draw()
		{
			GUILayout.BeginHorizontal(EditorStyles.helpBox, GUILayout.ExpandWidth(true),
									  GUILayout.ExpandHeight(true));

			GUILayout.EndHorizontal();
		}

		public void OnDisable() { }
	}
}
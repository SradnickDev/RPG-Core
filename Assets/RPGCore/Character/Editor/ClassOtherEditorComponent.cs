using RPGCore.Items;
using RPGCore.Items.Editor;
using UnityEngine;

namespace RPGCore.Character.Editor
{
	internal class ClassOtherEditorComponent : EditorComponent<CharacterClass>
	{
		public ClassOtherEditorComponent() { }

		public override void Draw()
		{
			if (Source == null) return;

			//TODO Add Error message if Definition is null

			DrawStats();
		}

		private void DrawStats()
		{
			GUILayout.Space(20);

			GUILayout.BeginVertical();

			GUILayout.EndHorizontal();
		}
	}
}
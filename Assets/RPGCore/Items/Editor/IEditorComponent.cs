using UnityEditor;

namespace RPGCore.Items.Editor
{
	internal interface IEditorComponent
	{
		void Draw(EditorWindow editorWindow);
	}
}
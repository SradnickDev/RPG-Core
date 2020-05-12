using UnityEditor;

namespace RPGCore.Items.Editor
{
	internal interface IEditorComponent
	{
		EditorWindow Window { get; set; }
		void OnEnable();
		void Draw();
		void OnDisable();
	}
}
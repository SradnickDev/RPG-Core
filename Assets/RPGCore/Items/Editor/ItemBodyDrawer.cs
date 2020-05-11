using UnityEditor;

namespace RPGCore.Items.Editor
{
	internal abstract class ItemBodyDrawer : IEditorComponent
	{
		public ItemDefinition Definition
		{
			get => m_itemDefinition;
			set => m_itemDefinition = value;
		}

		private ItemDefinition m_itemDefinition;
		public abstract void Draw(EditorWindow editorWindow);
	}
}
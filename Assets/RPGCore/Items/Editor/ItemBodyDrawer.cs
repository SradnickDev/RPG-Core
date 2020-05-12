using UnityEditor;

namespace RPGCore.Items.Editor
{
	internal abstract class ItemBodyDrawer : IEditorComponent
	{
		public EditorWindow Window { get; set; }

		public ItemDefinition Definition
		{
			get => m_itemDefinition;
			set => m_itemDefinition = value;
		}

		private ItemDefinition m_itemDefinition;
		public virtual void OnEnable() { }

		public abstract void Draw();
		public virtual void OnDisable() { }
	}
}
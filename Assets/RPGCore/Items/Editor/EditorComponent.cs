using UnityEditor;

namespace RPGCore.Items.Editor
{
	internal abstract class EditorComponent<T> : IEditorComponent
	{
		public EditorWindow Window { get; set; }

		public T Source
		{
			get => m_source;
			set => m_source = value;
		}

		private T m_source;
		public virtual void OnEnable() { }

		public abstract void Draw();
		public virtual void OnDisable() { }
	}
}
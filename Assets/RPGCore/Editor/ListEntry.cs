using RPGCore.Items.Editor;
using UnityEditor;
using UnityEngine;

namespace RPGCore.Editor
{
	public abstract class ListEntry : IEditorComponent
	{
		public EditorWindow Window { get; set; }

		private bool m_isSelected;

		public void OnEnable() { }

		public void DrawLabel(string name, string labelInfo, Texture2D icon, Color labelColor)
		{
			RpgEditorStyles.SelectableListEntry(m_isSelected ? RpgEditorStyles.SelectedLabel : RpgEditorStyles.DeselectedLabel,
									   icon, name, labelColor, labelInfo);
		}

		public abstract void Draw();

		public void Select()
		{
			m_isSelected = true;
		}

		public void Deselect()
		{
			m_isSelected = false;
		}

		public void OnDisable() { }
	}
}
using RPGCore.Items.Editor;
using UnityEditor;
using UnityEngine;

namespace RPGCore.Editor
{
	internal abstract class ResizeableEditor : IEditorComponent
	{
		public EditorWindow Window { get; set; }
		public float ListWidth => m_scrollViewWidth;

		private bool m_isResizing = false;
		private Rect m_changeRect;
		private const float SplitWidth = 5;
		private float m_horizontalSplitterPercent = 0.15f;
		private float m_scrollViewWidth = 250;

		public virtual void OnEnable()
		{
			m_changeRect = new Rect(m_scrollViewWidth,
									Window.position.y,
									SplitWidth,
									Window.position.height);
		}

		public abstract void Draw();

		protected void HandleHorizontalResize()
		{
			EditorGUIUtility.AddCursorRect(m_changeRect, MouseCursor.ResizeHorizontal);

			if (Event.current.type == EventType.MouseDown
			 && m_changeRect.Contains(Event.current.mousePosition))
			{
				m_isResizing = true;
			}


			if (m_isResizing)
			{
				m_horizontalSplitterPercent =
					Mathf.Clamp(Event.current.mousePosition.x / Window.position.width, 0.15f,
								0.5f);
				m_changeRect.x = (int) (Window.position.width * m_horizontalSplitterPercent);
				m_scrollViewWidth = m_changeRect.x;

				Window.Repaint();
			}

			if (Event.current.type == EventType.MouseUp)
			{
				m_isResizing = false;
			}
		}

		public virtual void OnDisable() { }
	}
}
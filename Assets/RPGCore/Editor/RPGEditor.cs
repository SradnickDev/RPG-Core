using System.Collections.Generic;
using RPGCore.Character.Editor;
using RPGCore.Items.Editor;
using UnityEditor;
using UnityEngine;

namespace RPGCore.Editor
{
	public class RPGEditor : EditorWindow
	{
		private const float DefaultWidth = 1200f;
		private const float DefaultHeight = 800f;
		private GUIContent[] m_tabs;

		private Dictionary<int, IEditorComponent> m_editors;

		private int m_currentTab = 0;

		[MenuItem("RPGCore/Editor")]
		public static void Open()
		{
			var editor = GetWindow<RPGEditor>();
			editor.maximized = false;
			editor.position = new Rect((Screen.width - DefaultWidth) / 2,
									   (Screen.height - DefaultHeight) / 2, DefaultWidth,
									   DefaultHeight);

			editor.titleContent = new GUIContent("RPG Core Editor");
			editor.Show();
		}

		private void OnEnable()
		{
			Initialize();
		}

		private void Initialize()
		{
			m_tabs = new[]
			{
				new GUIContent("Item DB", EditorGUIUtility.FindTexture("SoftlockInline")),
				new GUIContent("Character", EditorGUIUtility.FindTexture("SoftlockInline")),
				new GUIContent("Placeholder", EditorGUIUtility.FindTexture("SoftlockInline")),
				new GUIContent("Placeholder", EditorGUIUtility.FindTexture("SoftlockInline")),
			};
			m_editors = new Dictionary<int, IEditorComponent>
			{
				{0, new ItemEditor()},
				{1, new CharacterEditor()},
				{2, new CharacterEditor()},
				{3, new CharacterEditor()},
			};

			foreach (var editor in m_editors.Values)
			{
				editor.Window = this;
				editor.OnEnable();
			}
		}

		private void OnGUI()
		{
			m_currentTab =
				EditorExtension.Toolbar(m_currentTab, m_tabs,
										GUILayout.Height(35), GUILayout.ExpandWidth(true));
			GUILayout.BeginVertical();

			m_editors[m_currentTab].Draw();

			GUILayout.EndVertical();
		}

		private void OnDisable()
		{
			foreach (var editor in m_editors.Values)
			{
				editor.OnDisable();
			}
		}
	}
}
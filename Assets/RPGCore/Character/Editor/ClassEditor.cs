using RPGCore.Items.Editor;
using RPGCore.Utilities;
using UnityEditor;
using UnityEngine;

namespace RPGCore.Character.Editor
{
	internal class ClassEditor : IEditorComponent
	{
		public EditorWindow Window { get; set; }
		private ClassListEditor m_classList;
		private ClassBodyEditor m_bodyEditor;

		public ClassEditor()
		{
			m_classList = new ClassListEditor();
			m_bodyEditor = new ClassBodyEditor();
		}

		public void OnEnable()
		{
			m_classList.Window = Window;
			m_classList.OnEnable();

			m_bodyEditor.Window = Window;
			m_bodyEditor.OnEnable();

			m_classList.SelectionChanged += label => { m_bodyEditor.Set(label?.Source); };

			foreach (var charClass in GenericUtilities.FindAllDerivedTypes<CharacterClass>())
			{
				m_classList.Add((CharacterClass) System.Activator.CreateInstance(charClass));
			}
		}

		public void Draw()
		{
			GUILayout.BeginHorizontal(EditorStyles.helpBox, GUILayout.ExpandWidth(true),
									  GUILayout.ExpandHeight(true));
			m_classList.Draw();
			m_bodyEditor.Draw();
			GUILayout.EndHorizontal();
		}

		public void OnDisable()
		{
			m_classList.OnDisable();
			m_bodyEditor.OnDisable();
		}
	}
}
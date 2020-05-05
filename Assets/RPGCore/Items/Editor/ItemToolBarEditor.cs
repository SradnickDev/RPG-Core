using System;
using UnityEditor;
using UnityEngine;

namespace RPGCore.Items.Editor
{
	internal class ItemToolBarEditor : IEditorComponent
	{
		private ButtonAction m_loadBtn;
		private ButtonAction m_saveBtn;
		private ButtonAction m_createBtn;

		public ItemToolBarEditor(Action loadItems,
								 Action saveItems,
								 Action createItem)
		{
			m_loadBtn = new ButtonAction("Load Items", loadItems);
			m_saveBtn = new ButtonAction("Save Items", saveItems);
			m_createBtn = new ButtonAction("Create Item", createItem);
		}

		public void Draw(EditorWindow editorWindow)
		{
			DrawInternal(m_createBtn, new SpaceAction(), m_loadBtn, m_saveBtn);
		}

		private void DrawInternal(params FieldAction[] actions)
		{
			GUILayout.BeginHorizontal(EditorStyles.toolbar,
									  GUILayout.ExpandWidth(true),
									  GUILayout.MaxHeight(22));

			foreach (var btn in actions)
			{
				switch (btn)
				{
					case ButtonAction buttonAction:
						if (GUILayout.Button(btn.Name, EditorStyles.toolbarButton,
											 GUILayout.MaxWidth(88)))
						{
							buttonAction.Action?.Invoke();
						}

						break;
					case InputAction inputAction:
						GUILayout.BeginHorizontal();

						GUILayout.Label(new GUIContent(inputAction.Name));
						inputAction.Input = GUILayout.TextField(inputAction.Input,
																EditorStyles.toolbarTextField,
																GUILayout.MaxWidth(88));

						GUILayout.EndHorizontal();
						break;
					case SpaceAction spaceAction:
						spaceAction.Draw();
						break;
				}
			}

			GUILayout.EndHorizontal();
		}
	}
}
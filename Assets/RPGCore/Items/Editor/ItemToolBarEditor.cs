using System;
using UnityEditor;
using UnityEngine;

namespace RPGCore.Items.Editor
{
	internal class ItemToolBarEditor : IEditorComponent
	{
		public EditorWindow Window { get; set; }

		private ButtonAction m_loadBtn;
		private ButtonAction m_saveBtn;
		private ButtonAction m_createBtn;

		public ItemToolBarEditor(Action loadItems,
								 Action saveItems,
								 Action createItem)
		{
			m_loadBtn = new ButtonAction("Load Items", EditorGUIUtility.FindTexture("Asset Store"),
										 loadItems);
			m_saveBtn = new ButtonAction("Save Items", EditorGUIUtility.FindTexture("SaveActive"),
										 saveItems);
			m_createBtn = new ButtonAction("Create Item",
										   EditorGUIUtility.FindTexture("Toolbar Plus More"),
										   createItem);
		}

		public void OnEnable() { }

		public void Draw()
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
						if (buttonAction.GuiContent != null)
						{
							if (GUILayout.Button(buttonAction.GuiContent,
												 EditorStyles.toolbarButton,
												 GUILayout.MaxWidth(88)))
							{
								buttonAction.Action?.Invoke();
							}
						}
						else
						{
							if (GUILayout.Button(btn.Name, EditorStyles.toolbarButton,
												 GUILayout.MaxWidth(88)))
							{
								buttonAction.Action?.Invoke();
							}
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

		public void OnDisable() { }
	}
}
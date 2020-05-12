using System;
using System.Collections.Generic;
using System.Threading;
using RPGCore.Database.Item;
using RPGCore.Utilities;
using UnityEditor;
using UnityEngine;

namespace RPGCore.Items.Editor
{
	internal class ItemEditor : IEditorComponent
	{
		public EditorWindow Window { get; set; }

		private ItemToolBarEditor m_itemToolBarEditor;
		private ItemListEditor m_itemListEditor;
		private ItemBodyEditor m_itemBodyEditor;
		private bool m_loaded = false;

		public ItemEditor()
		{
			m_itemToolBarEditor = new ItemToolBarEditor(LoadItems, SaveItems, CreateItem);
			m_itemListEditor = new ItemListEditor();
			m_itemBodyEditor = new ItemBodyEditor();
		}

		public void OnEnable()
		{
			ItemDatabase.Initialize();

			m_itemListEditor.Window = Window;
			m_itemListEditor.OnEnable();

			m_itemBodyEditor.Window = Window;
			m_itemBodyEditor.OnEnable();

			m_itemListEditor.SelectionChanged += label =>
			{
				m_itemBodyEditor.Set(label?.ItemDefinition);
			};

			m_loaded = true;
		}

		public void LoadItems()
		{
			var token = new CancellationTokenSource();

			void DisplayProgress(int current, int max)
			{
				if (EditorUtility.DisplayCancelableProgressBar("Item Database",
															   $"Loading Items : {current} / {max}",
															   current / (float) max))
				{
					token.Cancel();
					EditorUtility.ClearProgressBar();
					token.Dispose();
					return;
				}

				if (current / (float) max >= 0.9f)
				{
					EditorUtility.ClearProgressBar();
				}
			}

			void OnCompleted(IEnumerable<ItemDefinition> result)
			{
				m_itemListEditor.Reset();
				m_itemListEditor.Set(result);
			}

			ItemDatabase.FetchAllASync(DisplayProgress, OnCompleted, token.Token);
		}

		public void SaveItems()
		{
			var items = m_itemListEditor.Items;
			ItemDatabase.Update(items);
		}

		public void CreateItem()
		{
			if (!m_loaded) return;

			var context = new GenericMenu();


			foreach (var type in GenericUtilities.FindAllDerivedTypes<ItemDefinition>())
			{
				var newItemTemplate = (ItemDefinition) Activator.CreateInstance(type);
				context.AddItem(new GUIContent(newItemTemplate.ReadableType()), false, () =>
				{
					newItemTemplate.DisplayName =
						$"new {newItemTemplate.ReadableType()}{(m_itemListEditor.ItemCount + 1)}";
					m_itemListEditor.Add(newItemTemplate);
				});
			}

			context.ShowAsContext();
		}

		public void Draw()
		{
			m_itemToolBarEditor.Draw();

			GUILayout.BeginHorizontal(GUILayout.ExpandWidth(true),
									  GUILayout.ExpandHeight(true));
			m_itemListEditor.Draw();
			m_itemBodyEditor.Draw();
			GUILayout.EndHorizontal();
		}

		public void OnDisable() { }
	}
}
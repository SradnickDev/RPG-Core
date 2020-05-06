using System;
using System.Collections.Generic;
using System.Threading;
using RPGCore.Database.Item;
using RPGCore.Utilities;
using UnityEditor;
using UnityEngine;

namespace RPGCore.Items.Editor
{
	internal class ItemEditor : EditorWindow
	{
		private ItemToolBarEditor m_itemToolBarEditor;
		private ItemListEditor m_itemListEditor;
		private ItemBodyEditor m_itemBodyEditor;
		private bool m_loaded = false;

		private const float DefaultWidth = 1200f;
		private const float DefaultHeight = 800f;

		[MenuItem("RPGCore/Item Editor")]
		public static void Open()
		{
			var editor = GetWindow<ItemEditor>();
			editor.maximized = false;
			editor.position = new Rect((Screen.width - DefaultWidth) / 2,
									   (Screen.height - DefaultHeight) / 2, DefaultWidth,
									   DefaultHeight);

			editor.titleContent = new GUIContent("Item Editor");
			editor.Show();
		}

		private void OnEnable()
		{
			Setup();
		}

		private void Setup()
		{
			ItemDatabase.Initialize();

			m_itemToolBarEditor = new ItemToolBarEditor(LoadItems, SaveItems, CreateItem);
			m_itemListEditor = new ItemListEditor(this);
			m_itemBodyEditor = new ItemBodyEditor();

			m_itemListEditor.SelectionChanged += label =>
			{
				m_itemBodyEditor.Set(label?.ItemTemplate);
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

			void OnCompleted(IEnumerable<ItemTemplate> result)
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


			foreach (var type in GenericUtilities.FindAllDerivedTypes<ItemTemplate>())
			{
				var newItemTemplate = (ItemTemplate) Activator.CreateInstance(type);
				context.AddItem(new GUIContent(newItemTemplate.ReadableType()), false, () =>
				{
					newItemTemplate.DisplayName =
						$"new {newItemTemplate.ReadableType()}{(m_itemListEditor.ItemCount + 1)}";
					m_itemListEditor.Add(newItemTemplate);
				});
			}

			context.ShowAsContext();
		}

		private void OnGUI()
		{
			m_itemToolBarEditor.Draw(this);

			GUILayout.BeginHorizontal(GUILayout.ExpandWidth(true),
									  GUILayout.ExpandHeight(true));
			m_itemListEditor.Draw(this);
			m_itemBodyEditor.Draw(this);
			GUILayout.EndHorizontal();
		}
	}
}
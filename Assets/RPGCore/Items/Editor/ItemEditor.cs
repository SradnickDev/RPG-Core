using System;
using RPGCore.Database.Item;
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

		[MenuItem("RPGCore/Item Editor")]
		public static void Open()
		{
			var editor = GetWindow<ItemEditor>();
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
			m_itemListEditor.Reset();
			var result = ItemDatabase.FetchAll();
			m_itemListEditor.Set(result);
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
				context.AddItem(new GUIContent(newItemTemplate.Name()), false, () =>
				{
					newItemTemplate.DisplayName =
						$"new {newItemTemplate.Name()}{(m_itemListEditor.ItemCount + 1)}";
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
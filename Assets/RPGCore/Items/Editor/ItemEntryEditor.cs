using RPGCore.Editor;
using RPGCore.Utilities;
using UnityEngine;

namespace RPGCore.Items.Editor
{
	public class ItemEntryEditor : ListEntry
	{
		public string ItemName => m_itemDefinition.DisplayName;
		public ItemDefinition ItemDefinition => m_itemDefinition;

		private ItemDefinition m_itemDefinition;

		
		public ItemEntryEditor(ItemDefinition itemDefinition)
		{
			Set(itemDefinition);
		}

		public void Set(ItemDefinition itemDefinition)
		{
			m_itemDefinition = itemDefinition;
		}

		private void UpdateLabelInformation()
		{
			DrawLabel(m_itemDefinition.DisplayName
					, m_itemDefinition.ReadableType()
					, m_itemDefinition.Icon.Data?.texture
					, m_itemDefinition.ItemColor());
		}

		public override void Draw()
		{
			if (m_itemDefinition == null)
			{
				Debug.LogError("ItemDefinition entry, ItemDefinition is null!");
				return;
			}

			UpdateLabelInformation();
		}
	}
}
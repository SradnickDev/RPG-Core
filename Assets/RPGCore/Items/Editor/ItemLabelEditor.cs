using RPGCore.Character.Editor;
using RPGCore.Editor;
using RPGCore.Utilities;
using UnityEngine;

namespace RPGCore.Items.Editor
{
	public class ItemLabelEditor : ListLabel
	{
		public string ItemName => m_itemDefinition.DisplayName;
		public ItemDefinition ItemDefinition => m_itemDefinition;

		private ItemDefinition m_itemDefinition;

		public ItemLabelEditor(ItemDefinition itemDefinition) : base()
		{
			Set(itemDefinition);
		}

		public void Set(ItemDefinition itemDefinition)
		{
			m_itemDefinition = itemDefinition;

			Set(m_itemDefinition.DisplayName
			  , m_itemDefinition.ReadableType()
			  , m_itemDefinition.Icon.Data?.texture
			  , m_itemDefinition.ItemColor());
		}

		public override void Draw()
		{
			if (m_itemDefinition == null)
			{
				Debug.LogError("Item Editor entry, ItemTemplate is null!");
				return;
			}

			UpdateLabelInformation();
			base.Draw();
		}

		private void UpdateLabelInformation() => Set(m_itemDefinition);
	}
}
using RPGCore.Items;
using RPGCore.Utilities;
using TMPro;
using UnityEngine;

namespace RPGCore.Inventory.Slots
{
	public class ItemSlot : BaseSlot
	{
		[SerializeField] private TextMeshProUGUI m_quantityLabel;

		public override void Add(IItem item)
		{
			Content = item;
			var quantity = item is StackableItem stackableItem ? stackableItem.Quantity : 0;
			Set(item.Definition.Icon.Data, item.Definition.ItemColor());
			m_quantityLabel.text = quantity == 0 ? "" : quantity.ToString();
		}

		public override void Refresh() => Add(Content);

		public override void Remove()
		{
			Content = null;
			m_quantityLabel.text = "";
			Clear();
		}

		public override bool CanDropItem(IItem selectedItem)
		{
			return true;
		}
	}
}
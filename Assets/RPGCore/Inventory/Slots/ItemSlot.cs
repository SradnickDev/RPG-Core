using RPGCore.Items;
using RPGCore.Utilities;
using TMPro;
using UnityEngine;

#pragma warning disable 0649
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
			OnAdded?.Invoke(item);
		}

		public override void Refresh() => Add(Content);

		public override void Remove()
		{
			OnRemoved?.Invoke(Content);
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
#pragma warning restore 0649
using RPGCore.Items;
using RPGCore.Utilities;

namespace RPGCore.Inventory.Slots
{
	public class ItemSlot : BaseSlot
	{
		public override void Add(IItem item)
		{
			Content = item;
			var quantity = item is StackableItem stackableItem ? stackableItem.Quantity : 0;
			Set(item.Definition.Icon.Data, item.Definition.ItemColor(), quantity);
		}
		

		public override void Refresh() => Add(Content);

		public override void Remove()
		{
			Content = null;
			Clear();
		}

		public override bool CanDropItem(IItem selectedItem)
		{
			return true;
		}
	}
}
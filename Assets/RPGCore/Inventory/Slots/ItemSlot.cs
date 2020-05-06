using RPGCore.Items;
using RPGCore.Utilities;

namespace RPGCore.Inventory.Slots
{
	public class ItemSlot : BaseSlot<IItem>
	{
		public override void Add(IItem item)
		{
			Content = item;
			var quantity = item is StackableItem stackableItem ? stackableItem.Quantity : 0;
			Set(item.ItemTemplate.Icon.Data, item.ItemTemplate.ItemColor(), quantity);
		}

		public override bool HasItem() => Content != null;

		public override bool IsEmpty(IItem item) => Content == null;

		public override void Refresh() => Add(Content);

		public override void Remove()
		{
			Content = null;
			Clear();
		}
	}
}
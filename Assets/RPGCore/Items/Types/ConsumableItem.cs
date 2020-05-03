namespace RPGCore.Items
{
	public class ConsumableItem : StackableItem
	{
		public ConsumableItem(ConsumableItemTemplate itemTemplate)
		{
			ItemTemplate = new ConsumableItemTemplate(itemTemplate);
		}

		public override IItem Duplicate()
		{
			return new ConsumableItem((ConsumableItemTemplate) ItemTemplate);
		}
	}
}
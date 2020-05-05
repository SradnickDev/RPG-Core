using RPGCore.Items.Types.Templates;

namespace RPGCore.Items.Types
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
namespace RPGCore.Items.Types
{
	public class ConsumableItem : StackableItem
	{
		public ConsumableItem(ConsumableItemDefinition itemDefinition)
		{
			Definition = new ConsumableItemDefinition(itemDefinition);
		}

		public override IItem Duplicate()
		{
			return new ConsumableItem((ConsumableItemDefinition) Definition);
		}
	}
}
namespace RPGCore.Items.Types
{
	public class ConsumableItemDefinition : ItemDefinition
	{
		public ConsumableItemDefinition() : base() { }
		public ConsumableItemDefinition(ConsumableItemDefinition itemDefinition) : base(itemDefinition) { }

		public ConsumableItem Initialize()
		{
			return new ConsumableItem(this);
		}
	}
}
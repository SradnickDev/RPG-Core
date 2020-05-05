namespace RPGCore.Items.Types.Templates
{
	public class ConsumableItemTemplate : ItemTemplate
	{
		public ConsumableItemTemplate() : base() { }
		public ConsumableItemTemplate(ConsumableItemTemplate itemTemplate) : base(itemTemplate) { }

		public ConsumableItem Initialize()
		{
			return new ConsumableItem(this);
		}
	}
}
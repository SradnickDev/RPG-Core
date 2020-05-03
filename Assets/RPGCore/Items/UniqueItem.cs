namespace RPGCore.Items
{
	public abstract class UniqueItem : IItem
	{
		public ItemTemplate ItemTemplate { get; set; }

		public abstract IItem Duplicate();
	}
}
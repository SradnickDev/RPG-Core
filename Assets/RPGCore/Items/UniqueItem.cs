namespace RPGCore.Items
{
	public abstract class UniqueItem : IItem
	{
		public ItemDefinition Definition { get; set; }

		public abstract IItem Duplicate();
	}
}
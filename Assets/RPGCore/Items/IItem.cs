namespace RPGCore.Items
{
	public interface IItem
	{
		ItemDefinition Definition { get; set; }

		IItem Duplicate();
	}
}
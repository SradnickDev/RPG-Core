namespace RPGCore.Items
{
	public interface IItem
	{
		ItemTemplate ItemTemplate { get; set; }

		IItem Duplicate();
	}
}
namespace RPGCore.Items
{
	public class MiscItem : StackableItem
	{
		public override IItem Duplicate()
		{
			return new MiscItem((MiscItemTemplate) ItemTemplate);
		}

		public MiscItem(MiscItemTemplate itemTemplate)
		{
			ItemTemplate = new MiscItemTemplate(itemTemplate);
		}
	}
}
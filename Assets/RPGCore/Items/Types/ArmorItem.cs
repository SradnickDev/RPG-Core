namespace RPGCore.Items
{
	public class ArmorItem : UniqueItem
	{
		public ArmorItem(ArmorItemTemplate itemTemplate)
		{
			ItemTemplate = new ArmorItemTemplate(itemTemplate);
		}

		public override IItem Duplicate()
		{
			return new ArmorItem((ArmorItemTemplate) ItemTemplate);
		}
	}
}
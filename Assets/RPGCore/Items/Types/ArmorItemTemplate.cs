namespace RPGCore.Items
{
	public class ArmorItemTemplate : ItemTemplate
	{
		public ArmorItemTemplate() : base() { }
		public ArmorItemTemplate(ArmorItemTemplate itemTemplate) : base(itemTemplate) { }

		public ArmorItem Initialize()
		{
			return new ArmorItem(this);
		}
	}
}
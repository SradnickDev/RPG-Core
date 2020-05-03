namespace RPGCore.Items
{
	public class MiscItemTemplate : ItemTemplate
	{
		public MiscItemTemplate() : base() { }
		public MiscItemTemplate(MiscItemTemplate itemTemplate) : base(itemTemplate) { }

		public MiscItem Initialize()
		{
			return new MiscItem(this);
		}
	}
}
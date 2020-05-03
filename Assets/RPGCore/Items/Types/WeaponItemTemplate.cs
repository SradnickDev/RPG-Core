namespace RPGCore.Items
{
	public class WeaponItemTemplate : ItemTemplate
	{
		public WeaponItemTemplate() : base() { }
		public WeaponItemTemplate(WeaponItemTemplate itemTemplate) : base(itemTemplate) { }

		public WeaponItem Initialize()
		{
			return new WeaponItem(this);
		}
	}
}
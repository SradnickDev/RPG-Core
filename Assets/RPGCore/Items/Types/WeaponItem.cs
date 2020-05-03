namespace RPGCore.Items
{
	public class WeaponItem : UniqueItem
	{
		public WeaponItem(WeaponItemTemplate itemTemplate)
		{
			ItemTemplate = new WeaponItemTemplate(itemTemplate);
		}

		public override IItem Duplicate()
		{
			return new WeaponItem((WeaponItemTemplate) ItemTemplate);
		}
	}
}
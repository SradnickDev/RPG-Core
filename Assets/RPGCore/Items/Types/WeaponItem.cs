using RPGCore.Items.Types.Templates;
using RPGCore.Stat;

namespace RPGCore.Items.Types
{
	public class WeaponItem : UniqueItem
	{
		public StatCollection Stats;

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
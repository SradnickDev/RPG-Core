using RPGCore.Stat;
using RPGCore.Stat.Types;

namespace RPGCore.Items.Types.Templates
{
	public class WeaponItemTemplate : ItemTemplate
	{
		public StatCollection Stats = new StatCollection();

		public WeaponItemTemplate() : base()
		{
		}

		public WeaponItemTemplate(WeaponItemTemplate itemTemplate) : base(itemTemplate)
		{
			Stats = itemTemplate.Stats;
		}

		public WeaponItem Initialize()
		{
			return new WeaponItem(this);
		}
	}
}
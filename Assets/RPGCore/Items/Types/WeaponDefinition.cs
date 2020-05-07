using RPGCore.Inventory.Slots;
using RPGCore.Stat;

namespace RPGCore.Items.Types
{
	public class WeaponDefinition : ItemDefinition
	{
		public WeaponType WeaponType;
		public StatCollection Stats = new StatCollection();

		public WeaponDefinition() : base() { }

		public WeaponDefinition(WeaponDefinition definition) : base(definition)
		{
			Stats = definition.Stats;
			WeaponType = definition.WeaponType;
		}

		public WeaponItem Initialize()
		{
			return new WeaponItem(this);
		}
	}
}
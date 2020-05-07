using RPGCore.Inventory.Slots;
using RPGCore.Stat;

namespace RPGCore.Items.Types
{
	public class ArmorDefinition : ItemDefinition
	{
		public ArmorType ArmorType;
		public StatCollection Stats = new StatCollection();

		public ArmorDefinition() : base() { }

		public ArmorDefinition(ArmorDefinition definition) : base(definition)
		{
			Stats = definition.Stats;
			ArmorType = definition.ArmorType;
		}

		public ArmorItem Initialize()
		{
			return new ArmorItem(this);
		}
	}
}
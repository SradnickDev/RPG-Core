using System.Collections.Generic;
using RPGCore.Database.Item;
using RPGCore.Inventory.Slots;
using RPGCore.Stat;

namespace RPGCore.Items.Types
{
	public class WeaponDefinition : ItemDefinition
	{
		public WeaponType WeaponType;
		public AnimOverrideControllerModel m_animatorOverrideController;

		public List<StatModifier> Stats = new List<StatModifier>();

		public WeaponDefinition() : base() { }

		public WeaponDefinition(WeaponDefinition definition) : base(definition)
		{
			Stats = definition.Stats;
			WeaponType = definition.WeaponType;
			m_animatorOverrideController = definition.m_animatorOverrideController;
		}

		public WeaponItem Initialize()
		{
			return new WeaponItem(this);
		}
	}
}
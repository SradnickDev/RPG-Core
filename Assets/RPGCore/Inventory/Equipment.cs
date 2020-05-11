using System;
using RPGCore.Inventory.Slots;
using RPGCore.Items;
using RPGCore.Items.Types;

namespace RPGCore.Inventory
{
	public class Equipment : SlotCollection
	{
		public BaseSlot this[ArmorType type] => Slots.Find(slot => Match(slot, type));
		private Character m_character;

		private bool Match(BaseSlot slot, ArmorType type)
		{
			if (slot is ArmorSlot armorSlot)
			{
				return armorSlot.ArmorType == type;
			}

			return false;
		}

		public void Setup(Character character)
		{
			m_character = character;
		}

		public override void OnItemAdded(IItem item)
		{
			var armor = (ArmorItem) item;
			var definition = (ArmorDefinition) armor.Definition;

			foreach (var statModifier in definition.Stats)
			{
				m_character.Stats[statModifier.Source]?.AddModifier(statModifier);
			}
		}

		public override void OnItemRemoved(IItem item)
		{
			var armor = (ArmorItem) item;
			var definition = (ArmorDefinition) armor.Definition;
			
			foreach (var statModifier in definition.Stats)
			{
				m_character.Stats[statModifier.Source]?.RemoveModifer(statModifier);
			}
		}
	}
}
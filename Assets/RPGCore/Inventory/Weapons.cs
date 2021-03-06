using RPGCore.Inventory.Slots;
using RPGCore.Items;
using RPGCore.Items.Types;

namespace RPGCore.Inventory
{
	public class Weapons : SlotCollection
	{
		public BaseSlot this[WeaponType type] => Slots.Find(slot => Match(slot, type));
		private Character.Character m_character;

		private bool Match(BaseSlot slot, WeaponType type)
		{
			if (slot is WeaponSlot weaponSlot)
			{
				return weaponSlot.WeaponType == type;
			}

			return false;
		}

		public void Setup(Character.Character character)
		{
			m_character = character;
		}

		public override void OnItemAdded(IItem item)
		{
			var weaponItem = (WeaponItem) item;
			var definition = (WeaponDefinition) weaponItem.Definition;

			foreach (var statModifier in definition.Stats)
			{
				m_character.Stats[statModifier.Source].AddModifier(statModifier);
			}
		}

		public override void OnItemRemoved(IItem item)
		{
			var weaponItem = (WeaponItem) item;
			var definition = (WeaponDefinition) weaponItem.Definition;

			foreach (var statModifier in definition.Stats)
			{
				m_character.Stats[statModifier.Source].RemoveModifer(statModifier);
			}
		}
	}
}
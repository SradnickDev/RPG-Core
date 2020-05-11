using RPGCore.Inventory.Slots;
using RPGCore.Items;

namespace RPGCore.Inventory
{
	public class Weapons : SlotCollection
	{
		public BaseSlot this[WeaponType type] => Slots.Find(slot => Match(slot, type));
		private Character m_character;

		private bool Match(BaseSlot slot, WeaponType type)
		{
			if (slot is WeaponSlot weaponSlot)
			{
				return weaponSlot.WeaponType == type;
			}

			return false;
		}

		public void Setup(Character character)
		{
			m_character = character;
		}

		public override void OnItemAdded(IItem item) { }

		public override void OnItemRemoved(IItem item) { }
	}
}
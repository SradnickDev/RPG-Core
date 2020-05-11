using RPGCore.Inventory.Slots;

namespace RPGCore.Inventory
{
	public class Weapons : SlotCollection
	{
		public BaseSlot this[WeaponType type] => Slots.Find(slot => Match(slot, type));

		private bool Match(BaseSlot slot, WeaponType type)
		{
			if (slot is WeaponSlot weaponSlot)
			{
				return weaponSlot.WeaponType == type;
			}

			return false;
		}
	}
}
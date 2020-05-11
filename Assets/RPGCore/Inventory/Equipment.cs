using RPGCore.Inventory.Slots;

namespace RPGCore.Inventory
{
	public class Equipment : SlotCollection
	{
		public BaseSlot this[ArmorType type] => Slots.Find(slot => Match(slot, type));

		private bool Match(BaseSlot slot, ArmorType type)
		{
			if (slot is ArmorSlot armorSlot)
			{
				return armorSlot.ArmorType == type;
			}

			return false;
		}
	}
}
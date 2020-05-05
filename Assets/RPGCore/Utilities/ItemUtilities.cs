using System;
using RPGCore.Inventory.Slots;
using RPGCore.Items;
using UnityEngine;

namespace RPGCore.Utilities
{
	public static class ItemUtilities
	{
		public static bool IsStackable(this IItem item) => item is StackableItem;

		public static bool IsStackableWith(this ItemSlot slot, IItem item)
		{
			var slotItem = slot.Content;
			var isSameItem = item.ItemTemplate.Id == slotItem.ItemTemplate.Id;
			return slotItem is StackableItem && isSameItem;
		}

		public static bool TryStackItem(this ItemSlot slot, IItem item)
		{
			var targetItem = ((StackableItem) slot.Content);
			var originItem = ((StackableItem) item);

			if (targetItem.CanStack(originItem.Quantity))
			{
				targetItem.Stack(originItem.Quantity);
				slot.Refresh();
				return true;
			}

			return false;
		}

		public static Color ItemColor(this ItemTemplate template)
		{
			switch (template.Rarity)
			{
				case Rarity.Poor:
					return new Color(0.62f, 0.62f, 0.62f);
				case Rarity.Common:
					return Color.white;
				case Rarity.Uncommon:
					return new Color(0.12f, 1.00f, 0);
				case Rarity.Rare:
					return new Color(0.00f, 0.44f, 0.87f);
				case Rarity.Epic:
					return new Color(0.64f, 0.21f, 0.93f);
				case Rarity.Legendary:
					return new Color(1, 0.5f, 0);
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		public static string ReadableType(this ItemTemplate template)
		{
			var itemType = template.GetType().Name;
			return itemType.Remove(itemType.Length - "ItemTemplate".Length, "ItemTemplate".Length);
		}

		public static bool TryGetTargetSlot(GameObject gbj, out ItemSlot targetSlot)
		{
			targetSlot = gbj != null ? gbj.GetComponent<ItemSlot>() : null;
			return targetSlot != null;
		}
	}
}
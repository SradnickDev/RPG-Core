using System.Collections.Generic;
using RPGCore.Inventory.Slots;
using UnityEngine;

namespace RPGCore.Inventory
{
	public class Bag : MonoBehaviour
	{
		public int Id
		{
			get => m_id;
			set => m_id = value;
		}

		private int m_id;
		public ItemSlot this[int idx] => m_itemSlots[idx];
		[SerializeField] private List<ItemSlot> m_itemSlots = new List<ItemSlot>();
	}
}
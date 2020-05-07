using System.Collections.Generic;
using RPGCore.Inventory.Slots;
using UnityEngine;

namespace RPGCore.Inventory
{
	public abstract class SlotCollection : MonoBehaviour
	{
		public int Id
		{
			get => m_id;
			set => m_id = value;
		}

		private int m_id;
		public BaseSlot this[int idx] => m_slots[idx];
		[SerializeField] private List<BaseSlot> m_slots = new List<BaseSlot>();
	}
}
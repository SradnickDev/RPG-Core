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
		public BaseSlot this[int idx] => Slots[idx];
		[SerializeField] protected List<BaseSlot> Slots = new List<BaseSlot>();
	}
}
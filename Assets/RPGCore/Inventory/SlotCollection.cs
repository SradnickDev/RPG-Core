using System.Collections.Generic;
using RPGCore.Inventory.Slots;
using RPGCore.Items;
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

		private void OnEnable()
		{
			foreach (var slot in Slots)
			{
				slot.OnAdded = OnItemAdded;
				slot.OnRemoved = OnItemRemoved;
			}
		}

		private void OnDisable()
		{
			foreach (var slot in Slots)
			{
				slot.OnAdded = null;
				slot.OnRemoved = null;
			}
		}

		public abstract void OnItemAdded(IItem item);
		public abstract void OnItemRemoved(IItem item);
	}
}
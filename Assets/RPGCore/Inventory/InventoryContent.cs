using System.Collections.Generic;
using RPGCore.Items;
using UnityEngine;

namespace RPGCore.Inventory
{
	public class InventoryContent : MonoBehaviour
	{
		[SerializeField] private List<ItemDefinition> m_items = new List<ItemDefinition>();
	}
}
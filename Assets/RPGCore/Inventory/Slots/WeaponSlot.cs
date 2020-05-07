using RPGCore.Items;
using RPGCore.Items.Types;
using RPGCore.Utilities;
using UnityEngine;
using UnityEngine.UI;

#pragma warning disable 0649
namespace RPGCore.Inventory.Slots
{
	public class WeaponSlot : BaseSlot
	{
		[SerializeField] private WeaponType m_slotType;
		[SerializeField] private Image m_typeIcon;
		public override void Refresh() => Add(Content);

		public override void Add(IItem item)
		{
			var definition = (WeaponDefinition) item.Definition;

			if (definition.WeaponType == m_slotType)
			{
				Content = item;
				Set(definition.Icon.Data, definition.ItemColor(), 0);
				m_typeIcon.enabled = false;
			}
		}

		public override bool CanDropItem(IItem selectedItem)
		{
			if (selectedItem is WeaponItem weaponItem)
			{
				var definition = (WeaponDefinition) weaponItem.Definition;
				return definition.WeaponType == m_slotType;
			}

			return false;
		}

		public override void Remove()
		{
			m_typeIcon.enabled = false;
			Content = null;
			Clear();
		}
	}
}
#pragma warning restore 0649
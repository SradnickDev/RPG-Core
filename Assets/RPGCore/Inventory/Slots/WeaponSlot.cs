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
		public WeaponType WeaponType => m_weaponType;
		[SerializeField] private WeaponType m_weaponType;
		[SerializeField] private Image m_typeIcon;
		public override void Refresh() => Add(Content);

		public override void Add(IItem item)
		{
			var definition = (WeaponDefinition) item.Definition;

			if (definition.WeaponType == m_weaponType)
			{
				Content = item;
				Set(definition.Icon.Data, definition.ItemColor());
				m_typeIcon.enabled = false;
			}
		}

		public override bool CanDropItem(IItem selectedItem)
		{
			if (selectedItem is WeaponItem weaponItem)
			{
				var definition = (WeaponDefinition) weaponItem.Definition;
				return definition.WeaponType == m_weaponType;
			}

			return false;
		}

		public override void Remove()
		{
			m_typeIcon.enabled = true;
			Content = null;
			Clear();
		}
	}
}
#pragma warning restore 0649
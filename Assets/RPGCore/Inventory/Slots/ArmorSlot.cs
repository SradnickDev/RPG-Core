using RPGCore.Items;
using RPGCore.Items.Types;
using RPGCore.Utilities;
using UnityEngine;
using UnityEngine.UI;

#pragma warning disable 0649
namespace RPGCore.Inventory.Slots
{
	public class ArmorSlot : BaseSlot
	{
		public ArmorType ArmorType => m_armorType;
		[SerializeField] private ArmorType m_armorType;
		[SerializeField] private Image m_typeIcon;
		public override void Refresh() => Add(Content);

		public override void Add(IItem item)
		{
			var definition = (ArmorDefinition) item.Definition;

			if (definition.ArmorType == m_armorType)
			{
				Content = item;
				Set(definition.Icon.Data, definition.ItemColor());
				m_typeIcon.enabled = false;
			}
		}

		public override bool CanDropItem(IItem selectedItem)
		{
			if (selectedItem is ArmorItem weaponItem)
			{
				var definition = (ArmorDefinition) weaponItem.Definition;
				return definition.ArmorType == m_armorType;
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
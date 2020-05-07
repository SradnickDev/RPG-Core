using RPGCore.Inventory.Slots;
using RPGCore.Items;
using RPGCore.Items.Types;
using RPGCore.Stat;
using RPGCore.Utilities;
using TMPro;
using UnityEngine;

#pragma warning disable 0649
namespace RPGCore.Inventory
{
	public class ItemPreviewPopup : MonoBehaviour
	{
		public RectTransform RectTransform
		{
			get
			{
				if (m_rectTransform == null)
				{
					m_rectTransform = transform as RectTransform;
				}

				return m_rectTransform;
			}
		}

		private RectTransform m_rectTransform;
		[SerializeField] private TextMeshProUGUI m_headerLabel;
		[SerializeField] private TextMeshProUGUI m_contentLabel;

		public void Show(BaseSlot slot, Vector2 position)
		{
			gameObject.SetActive(true);

			var itemTemplate = slot.Content.Definition;
			RectTransform.position = position;

			UpdateItemInfos(itemTemplate);
		}

		private void UpdateItemInfos(ItemDefinition itemDefinition)
		{
			SetDefaultInfo(itemDefinition);
			SetStats(itemDefinition);
			SetDescription(itemDefinition);
		}

		private void SetDefaultInfo(ItemDefinition itemDefinition)
		{
			m_headerLabel.text = itemDefinition.DisplayName;
			m_headerLabel.color = itemDefinition.ItemColor();
			m_contentLabel.text = $"{itemDefinition.ReadableType()} - {itemDefinition.Rarity}";
			m_contentLabel.text += "\n";
		}

		private void SetStats(ItemDefinition itemDefinition)
		{
			StatCollection stats = null;

			if (itemDefinition.GetType() == typeof(WeaponDefinition))
			{
				stats = ((WeaponDefinition) itemDefinition).Stats;
			}

			if (itemDefinition.GetType() == typeof(ArmorDefinition))
			{
				stats = ((ArmorDefinition) itemDefinition).Stats;
			}

			if (stats != null)
			{
				foreach (var stat in stats)
				{
					m_contentLabel.text += $"+{stat.Value} {stat.GetType().Name} \n";
				}

				m_contentLabel.text += "\n";
			}
		}

		private void SetDescription(ItemDefinition itemDefinition)
		{
			m_contentLabel.text += itemDefinition.Description;
		}

		public void Hide()
		{
			gameObject.SetActive(false);
		}
	}
}
#pragma warning restore 0649
using RPGCore.Inventory.Slots;
using RPGCore.Items;
using RPGCore.Items.Types.Templates;
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

		public void Show(ItemSlot slot, Vector2 position)
		{
			gameObject.SetActive(true);

			var itemTemplate = slot.Content.ItemTemplate;
			RectTransform.position = position;

			UpdateItemInfos(itemTemplate);
		}

		private void UpdateItemInfos(ItemTemplate itemTemplate)
		{
			SetDefaultInfo(itemTemplate);
			SetStats(itemTemplate);
			SetDescription(itemTemplate);
		}

		private void SetDefaultInfo(ItemTemplate itemTemplate)
		{
			m_headerLabel.text = itemTemplate.DisplayName;
			m_headerLabel.color = itemTemplate.ItemColor();
			m_contentLabel.text = $"{itemTemplate.ReadableType()} - {itemTemplate.Rarity}";
			m_contentLabel.text += "\n";
		}

		private void SetStats(ItemTemplate itemTemplate)
		{
			StatCollection stats = null;

			if (itemTemplate.GetType() == typeof(WeaponItemTemplate))
			{
				stats = ((WeaponItemTemplate) itemTemplate).Stats;
			}

			if (itemTemplate.GetType() == typeof(ArmorItemTemplate))
			{
				stats = ((ArmorItemTemplate) itemTemplate).Stats;
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

		private void SetDescription(ItemTemplate itemTemplate)
		{
			m_contentLabel.text += itemTemplate.Description;
		}

		public void Hide()
		{
			gameObject.SetActive(false);
		}
	}
}
#pragma warning restore 0649
using RPGCore.Items;
using TMPro;
using UnityEngine;

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
		m_headerLabel.text = itemTemplate.DisplayName;
		m_headerLabel.color = itemTemplate.ItemColor();
		m_contentLabel.text = itemTemplate.Description;
	}

	public void Hide()
	{
		gameObject.SetActive(false);
	}
}
using TMPro;
using UnityEngine;
using UnityEngine.UI;

#pragma warning disable 0649
public abstract class BaseSlot<T> : MonoBehaviour, ISlot<T>
{
	[SerializeField] private Image m_icon;
	[SerializeField] private Image m_border;
	[SerializeField] private TextMeshProUGUI m_quantityLabel;
	public T Content;

	public void Set(Sprite icon, Color borderColor, int quantity)
	{
		m_icon.sprite = icon;
		m_icon.color = Color.white;
		m_border.color = borderColor;
		m_quantityLabel.text = quantity == 0 ? "" : quantity.ToString();
	}

	public abstract void Refresh();
	public void Clear()
	{
		m_icon.sprite = null;
		m_icon.color = Color.clear;
		m_border.color = Color.clear;
		m_quantityLabel.text = "";
	}

	public abstract void Add(T item);

	public abstract bool HasItem();

	public abstract bool IsEmpty(T item);

	public abstract void Remove();
}
#pragma warning restore 0649
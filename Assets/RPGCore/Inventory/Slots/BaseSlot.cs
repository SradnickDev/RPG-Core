using RPGCore.Items;
using UnityEngine;
using UnityEngine.UI;

#pragma warning disable 0649
namespace RPGCore.Inventory.Slots
{
	public abstract class BaseSlot : MonoBehaviour
	{
		public virtual bool IsEmpty => Content == null;

		[SerializeField] private Image m_icon;
		[SerializeField] private Image m_border;
		public IItem Content;

		public void Set(Sprite icon, Color borderColor)
		{
			m_icon.sprite = icon;
			m_icon.color = Color.white;
			m_border.color = borderColor;
		}

		public abstract void Refresh();

		public void Clear()
		{
			m_icon.sprite = null;
			m_icon.color = Color.clear;
			m_border.color = Color.clear;
		}

		public abstract void Add(IItem item);

		public abstract void Remove();

		public abstract bool CanDropItem(IItem selectedItem);
	}
}
#pragma warning restore 0649
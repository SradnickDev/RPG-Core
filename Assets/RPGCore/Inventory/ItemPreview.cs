using RPGCore.Inventory.Slots;
using RPGCore.UI;
using RPGCore.Utilities;
using UnityEngine;
using UnityEngine.UI;

#pragma warning disable 0649
namespace RPGCore.Inventory
{
	public class ItemPreview : MonoBehaviour
	{
		[SerializeField] private GraphicRaycaster m_raycaster;
		[SerializeField] private ItemPreviewPopup m_previewPopup;
		[SerializeField] private RectAnchor m_rectAnchor = RectAnchor.Left;
		[SerializeField] private float m_offset = 5;

		private BaseSlot m_currentTarget;
		private bool m_enablePreview = true;
		private GraphicHoverTrigger m_hoverTrigger;

		private void Start()
		{
			m_hoverTrigger = new GraphicHoverTrigger(m_raycaster, OnPointerEnter, OnPointerExit);
		}

		private void Update() => m_hoverTrigger.Update();

		public void OnPointerEnter(GameObject gbj)
		{
			if (!m_enablePreview) return;

			if (ItemUtilities.TryGetSlot(gbj, out m_currentTarget))
			{
				if (!m_currentTarget.IsEmpty)
				{
					var popUpPosition =
						UiUtilities.AnchoredPosition((RectTransform) m_currentTarget.transform,
													 m_previewPopup.RectTransform,
													 m_rectAnchor, m_offset);

					m_previewPopup.transform.SetAsLastSibling();
					m_previewPopup.Show(m_currentTarget, popUpPosition);
				}
			}
		}

		public void OnPointerExit(GameObject gbj)
		{
			m_previewPopup.Hide();
			m_currentTarget = null;
		}

		public void EnablePreview()
		{
			m_enablePreview = true;
			m_currentTarget = null;
			m_hoverTrigger.Reset();
		}

		public void DisablePreview()
		{
			m_enablePreview = false;
			m_currentTarget = null;
			m_previewPopup.Hide();
		}
	}
}
#pragma warning restore 0649
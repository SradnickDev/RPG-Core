﻿using RPGCore.Items;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GraphicRaycaster))]
public class ItemPreview : MonoBehaviour
{
	[SerializeField] private GraphicRaycaster m_raycaster;
	[SerializeField] private ItemPreviewPopup m_previewPopup;
	[SerializeField] private RectAnchor m_rectAnchor = RectAnchor.Left;
	[SerializeField] private float m_offset = 5;

	private ItemSlot m_currentTarget;
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

		if (ItemUtilities.TryGetTargetSlot(gbj, out m_currentTarget))
		{
			if (m_currentTarget.HasItem())
			{
				var popUpPosition =
					UiUtilities.AnchoredPosition((RectTransform) m_currentTarget.transform,
												 m_previewPopup.RectTransform,
												 m_rectAnchor, m_offset);

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
		m_hoverTrigger.Reset();
	}

	public void DisablePreview()
	{
		m_enablePreview = false;
		m_currentTarget = null;
		m_previewPopup.Hide();
	}
}
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GraphicHoverTrigger
{
	private event Action<GameObject> OnEnter;
	private event Action<GameObject> OnExit;
	
	private GraphicRaycaster m_raycaster;
	private PointerEventData m_pointerEventData;
	private GameObject m_pointerTarget;
	private List<RaycastResult> m_raycastResults = new List<RaycastResult>();

	public GraphicHoverTrigger(GraphicRaycaster raycaster,
							   Action<GameObject> enter,
							   Action<GameObject> exit)
	{
		m_raycaster = raycaster;
		m_pointerEventData = new PointerEventData(EventSystem.current);
		OnEnter = enter;
		OnExit = exit;
	}

	public void Update() => PointerTargetUpdate();

	private void PointerTargetUpdate()
	{
		Raycast();

		if (m_raycastResults.Count <= 0)
		{
			TriggerExitCallback(m_pointerTarget);

			m_pointerTarget = null;
			return;
		}

		NewTarget();
	}

	private void NewTarget()
	{
		if (m_raycastResults[0].gameObject != m_pointerTarget)
		{
			var newSelection = m_raycastResults[0].gameObject;

			TriggerExitCallback(m_pointerTarget);

			TriggerEnterCallback(newSelection);

			m_pointerTarget = newSelection;
		}
	}

	private void Raycast()
	{
		m_raycastResults = new List<RaycastResult>();
		m_pointerEventData.position = Input.mousePosition;
		m_raycaster.Raycast(m_pointerEventData, m_raycastResults);
	}

	private void TriggerEnterCallback(GameObject newSelection)
	{
		OnEnter?.Invoke(newSelection);
	}

	private void TriggerExitCallback(GameObject pointerTarget)
	{
		OnExit?.Invoke(pointerTarget);
	}

	public void Reset()
	{
		m_pointerTarget = null;
		m_raycastResults = new List<RaycastResult>();
		PointerTargetUpdate();
	}
}
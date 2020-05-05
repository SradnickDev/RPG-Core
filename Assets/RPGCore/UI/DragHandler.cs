using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

#pragma warning disable 0649
namespace RPGCore.UI
{
	[RequireComponent(typeof(GraphicRaycaster))]
	public class DragHandler : MonoBehaviour
	{
		[SerializeField] private List<DragWindow> m_dragWindows;
		private GraphicRaycaster m_raycaster;
		private PointerEventData m_pointerEventData;
		private DragWindow m_pointerTarget;
		private List<RaycastResult> m_raycastResults = new List<RaycastResult>();
		private Vector3 m_previousPointerPosition;
		private DragEventData m_eventData;

		private void Start()
		{
			m_raycaster = GetComponent<GraphicRaycaster>();
			m_pointerEventData = new PointerEventData(EventSystem.current);
		}

		public void Update()
		{
			//TODO refactore
			m_raycastResults = new List<RaycastResult>();
			m_pointerEventData.position = Input.mousePosition;
			m_raycaster.Raycast(m_pointerEventData, m_raycastResults);

			if (Input.GetKeyDown(KeyCode.Mouse0))
			{
				m_previousPointerPosition = Input.mousePosition;
				foreach (var hit in m_raycastResults)
				{
					if (hit.gameObject.TryGetComponent<DragArea>(out var target))
					{
						if (target != null)
						{
							m_eventData = new DragEventData()
							{
								Drag = hit.gameObject,
								Delta = Input.mousePosition,
								Pointer = Input.mousePosition,
								PointerBegin = Input.mousePosition
							};
							m_pointerTarget = target.TargetWindow;
							m_pointerTarget.OnBeginDrag(m_eventData);
							m_pointerTarget.transform.SetAsLastSibling();
							break;
						}
					}
				}
			}

			var isMoving = (Input.mousePosition - m_previousPointerPosition).sqrMagnitude >= 0.0f;
			if (m_pointerTarget != null && isMoving)
			{
				m_eventData.Delta = Input.mousePosition - m_previousPointerPosition;
				m_eventData.Pointer = Input.mousePosition;

				foreach (var window in m_dragWindows)
				{
					if (!window.gameObject.activeInHierarchy) continue;
					if (window.gameObject != m_pointerTarget.gameObject
					 && m_pointerTarget.OverlapsWith(window))
					{
						m_pointerTarget.CollideWith(window);
					}
				}

				m_pointerTarget.OnDrag(m_eventData);
				m_previousPointerPosition = Input.mousePosition;
			}

			if (Input.GetKeyUp(KeyCode.Mouse0))
			{
				m_pointerTarget?.OnEndDrag(m_eventData);
				m_pointerTarget = null;
			}
		}
	}
}
#pragma warning restore 0649
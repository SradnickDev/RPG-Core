using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

#pragma warning disable 0649
namespace RPGCore.UI
{
	[RequireComponent(typeof(GraphicRaycaster))]
	public class DragHandler : MonoBehaviour
	{
		public UnityEvent OnDrag;
		public UnityEvent OnDrop;
		public Vector2 PointerDelta => Input.mousePosition - m_previousPointerPosition;
		public bool IsMoving => PointerDelta.sqrMagnitude >= 0.0f;

		[SerializeField] private List<DragWindow> m_dragWindows;
		[SerializeField] private bool m_detectCollision;
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
			UpdateRaycastResults();

			DetectDragStart();

			UpdateDrag();

			DetectDragEnd();
		}

		/// <summary>
		/// Using UI Raycast to create a list of possible targets.
		/// </summary>
		private void UpdateRaycastResults()
		{
			m_raycastResults = new List<RaycastResult>();
			m_pointerEventData.position = Input.mousePosition;
			m_raycaster.Raycast(m_pointerEventData, m_raycastResults);
		}

		private void DetectDragStart()
		{
			if (!Input.GetKeyDown(KeyCode.Mouse0)) return;

			//in case drag end wasn't called properly
			if (m_pointerTarget != null)
			{
				m_pointerTarget?.OnEndDrag(m_eventData);
				m_pointerTarget = null;
			}

			m_previousPointerPosition = Input.mousePosition;
			foreach (var hit in m_raycastResults)
			{
				if (hit.gameObject.TryGetComponent<DragArea>(out var target))
				{
					if (target == null) continue;

					UpdateEventData(target.gameObject);
					m_eventData.PointerBegin = Input.mousePosition;

					m_pointerTarget = target.TargetWindow;
					m_pointerTarget.OnBeginDrag(m_eventData);
					m_pointerTarget.transform.SetAsLastSibling();

					break;
				}
			}

			OnDrag?.Invoke();
		}

		private void UpdateDrag()
		{
			if (m_pointerTarget != null && IsMoving)
			{
				UpdateEventData(m_eventData.Drag);

				DetectCollision();

				m_pointerTarget.OnDrag(m_eventData);
				m_previousPointerPosition = Input.mousePosition;
			}
		}

		private void DetectCollision()
		{
			if (!m_detectCollision) return;

			foreach (var window in m_dragWindows
				.Where(window => window.gameObject.activeInHierarchy))
			{
				if (window.gameObject != m_pointerTarget.gameObject
				 && m_pointerTarget.OverlapsWith(window))
				{
					m_pointerTarget.CollideWith(window);
				}
			}
		}

		private void DetectDragEnd()
		{
			if (Input.GetKeyUp(KeyCode.Mouse0))
			{
				m_pointerTarget?.OnEndDrag(m_eventData);
				m_pointerTarget = null;
				UpdateEventData(null);
				OnDrop?.Invoke();
			}
		}

		private void UpdateEventData(GameObject dragGameObject)
		{
			if (m_eventData == null)
			{
				m_eventData = new DragEventData();
			}

			m_eventData.Drag = dragGameObject;
			m_eventData.PointerDelta = PointerDelta;
			m_eventData.PointerCurrent = Input.mousePosition;
			m_eventData.IsMoving = IsMoving;
		}
	}
}
#pragma warning restore 0649
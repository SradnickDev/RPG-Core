using UnityEngine;
using UnityEngine.UI;

#pragma warning disable 0649
namespace RPGCore.UI
{
	public class DragWindow : MonoBehaviour, IDragable
	{
		[SerializeField] private Button m_closeButton;

		public RectTransform RectTransform
		{
			get
			{
				if (m_rectTransform == null)
				{
					m_rectTransform = GetComponent<RectTransform>();
				}

				return m_rectTransform;
			}
		}

		private RectTransform m_rectTransform;

		public Vector3[] Corners
		{
			get
			{
				var corners = new Vector3[4];
				RectTransform.GetWorldCorners(corners);
				return corners;
			}
		}

		public Rect Rect => RectTransform.rect;
		private Vector3 m_previousPosition = Vector3.zero;

		private void Start()
		{
			RegisterCloseEvent();
		}

		private void RegisterCloseEvent()
		{
			if (m_closeButton)
			{
				m_closeButton.onClick.AddListener(() => { this.gameObject.SetActive(false); });
			}
		}

		public void OnBeginDrag(DragEventData eventData) { }

		public bool OverlapsWith(DragWindow panel)
		{
			var a = GetWorldRect(RectTransform, Vector2.one);
			var b = GetWorldRect(panel.RectTransform, Vector2.one);

			return b.xMax > a.xMin && b.xMin < a.xMax && b.yMax > a.yMin && b.yMin < a.yMax;
		}

		public void OnDrag(DragEventData eventData)
		{
			m_previousPosition = RectTransform.position;
			RectTransform.position += eventData.Delta;
			KeepInScreen();
		}

		public void OnEndDrag(DragEventData eventData) { }

		private void KeepInScreen()
		{
			var prevPos = RectTransform.position;
			var screenSize = new Vector3(Screen.width, Screen.height);
			var worldSpaceRectMin = RectTransform.TransformPoint(Rect.min);
			var worldSpaceRectMax = RectTransform.TransformPoint(Rect.max);
			var max = screenSize - (worldSpaceRectMax - worldSpaceRectMin);

			var x = worldSpaceRectMin.x;
			var y = worldSpaceRectMin.y;
			x = Mathf.Clamp(x, 0, max.x);
			y = Mathf.Clamp(y, 0, max.y);

			RectTransform.position = new Vector3(x, y, 0) + prevPos - worldSpaceRectMin;
		}

		public void CollideWith(DragWindow window)
		{
			RectTransform.position = m_previousPosition;
		}

		public static Rect GetWorldRect(RectTransform rt, Vector2 scale)
		{
			var corners = new Vector3[4];
			rt.GetWorldCorners(corners);
			var topLeft = corners[1];

			var scaledSize = new Vector2(scale.x * rt.rect.size.x, scale.y * rt.rect.size.y);

			return new Rect(topLeft, scaledSize);
		}
	}
}
#pragma warning restore 0649
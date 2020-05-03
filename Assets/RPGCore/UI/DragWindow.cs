using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class DragWindow : MonoBehaviour, IDragable
{
	public UnityEvent Drag;
	public UnityEvent Drop;

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

	public void OnBeginDrag(DragEventData eventData)
	{
		Drag?.Invoke();
	}
	
	public bool OverlapsWith(DragWindow panel)
	{ 
		var a = GetWorldRect(RectTransform, Vector2.one);
		var b = GetWorldRect(panel.RectTransform, Vector2.one);

		return  b.xMax > a.xMin && b.xMin < a.xMax && b.yMax > a.yMin && b.yMin < a.yMax;
	}

	public void OnDrag(DragEventData eventData)
	{
		RectTransform.position += eventData.Delta;
		KeepInScreen();
	}

	public void OnEndDrag(DragEventData eventData)
	{
		Drop?.Invoke();
	}

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

	private Rect result = new Rect();
	public void CollideWith(DragWindow window)
	{
		var a = GetWorldRect(RectTransform, Vector2.one);
		var b = GetWorldRect(window.RectTransform, Vector2.one);

		var x = Math.Max(a.x, b.x);
		var y = Math.Max(a.y, b.y);
		
		var num1 = Math.Min(a.x + a.width, b.x + b.width);
		var num2 = Math.Min(a.y + a.height, b.y + b.height);
		
		if (num1 >= x && num2 >= y)
		{
			result = new Rect(x, y, num1 - x, num2 - y);
		}
		else
		{
			result = new Rect(0,0,0,0);	
		}
	}

	private void OnGUI()
	{
		GUI.Window(123,result, id => {},"asd");
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
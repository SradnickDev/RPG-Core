using UnityEngine;
using UnityEngine.UI;

public class DragArea : MonoBehaviour
{
	public DragWindow TargetWindow => m_targetWindow;

	[SerializeField]
	private DragWindow m_targetWindow;
}
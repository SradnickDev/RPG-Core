using UnityEngine;

namespace RPGCore.UI
{
	public class DragArea : MonoBehaviour
	{
		public DragWindow TargetWindow => m_targetWindow;

		[SerializeField] private DragWindow m_targetWindow = null;
	}
}
using UnityEngine;

namespace RPGCore.UI
{
	public class DragEventData
	{
		public GameObject Drag { get; set; }
		public Vector3 PointerBegin { get; set; }
		public Vector3 PointerCurrent { get; set; }
		public Vector3 PointerDelta { get; set; }
		public bool IsMoving { get; set; }
	}
}
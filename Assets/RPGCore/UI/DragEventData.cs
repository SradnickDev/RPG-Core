using UnityEngine;

public class DragEventData
{
	public GameObject Drag { get; set; }
	public Vector3 PointerBegin { get; set; }
	public Vector3 Pointer { get; set; }
	public Vector3 Delta { get; set; }
}
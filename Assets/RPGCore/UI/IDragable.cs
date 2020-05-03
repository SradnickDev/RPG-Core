public interface IDragable
{
	void OnBeginDrag(DragEventData eventData);
	void OnDrag(DragEventData eventData);
	void OnEndDrag(DragEventData eventData);
}
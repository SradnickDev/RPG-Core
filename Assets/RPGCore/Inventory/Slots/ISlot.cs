namespace RPGCore.Inventory.Slots
{
	public interface ISlot<T>
	{
		void Add(T item);
		bool HasItem();
		bool IsEmpty(T item);
		void Remove();
	}
}
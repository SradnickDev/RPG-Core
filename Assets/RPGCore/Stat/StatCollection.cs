using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RPGCore.Stat
{
	[System.Serializable]
	public class StatCollection : ICollection<BaseStat>
	{
		public int Count => m_items.Count;

		public bool IsReadOnly => false;

		private readonly HashSet<BaseStat> m_items;

		public BaseStat this[string stat] =>
			m_items.FirstOrDefault(x => string.Equals(x.GetType().Name, stat));

		public BaseStat this[int index] => m_items.ElementAt(index);

		public StatCollection()
		{
			m_items = new HashSet<BaseStat>();
		}

		public StatCollection(HashSet<BaseStat> other)
		{
			m_items = new HashSet<BaseStat>(other);
		}

		public void Add(BaseStat item)
		{
			m_items.Add(item);
		}

		public void Clear()
		{
			m_items.Clear();
		}

		public bool Contains(BaseStat item) => m_items.Contains(item);

		public void CopyTo(BaseStat[] array, int arrayIndex)
		{
			m_items.CopyTo(array, arrayIndex);
		}

		public bool Remove(BaseStat item) => m_items.Remove(item);

		public IEnumerator<BaseStat> GetEnumerator() => m_items.GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator() => m_items.GetEnumerator();
	}
}
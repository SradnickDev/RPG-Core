using System.Collections.Generic;
using UnityEngine;

namespace RPGCore.Stat
{
	public abstract class BaseStat
	{
		public float Value
		{
			get
			{
				if (m_isDirty)
				{
					Calculate();
					m_isDirty = false;
				}

				return m_cachedValue;
			}
		}

		public float BaseValue = 0;
		public float AdditionalValue => m_cachedValue - BaseValue;
		private bool m_isDirty = true;

		private readonly List<StatModifier> m_modifiers = new List<StatModifier>();
		private float m_cachedValue;
		public Growth Growth = new Growth();

		public BaseStat() { }

		private void Calculate()
		{
			var retVal = BaseValue;

			foreach (var modifier in m_modifiers)
			{
				switch (modifier.ModifierType)
				{
					case ModifierType.Constant:
						retVal += modifier.Value;
						break;
					case ModifierType.PercentOnDefault:
						retVal += BaseValue * (modifier.Value / 100f);
						break;
					case ModifierType.PercentOnCurrent:
						retVal *= 1 + (modifier.Value / 100f);
						break;
				}
			}


			m_cachedValue = Mathf.Round(retVal * 10f) / 10f;
		}

		public void AddModifier(StatModifier statModifier)
		{
			m_isDirty = true;
			m_modifiers.Add(statModifier);
			m_modifiers.Sort((a, b) => a.ModifierType.CompareTo(b.ModifierType));
		}

		public void RemoveModifer(StatModifier statModifier)
		{
			if (m_modifiers.Remove(statModifier))
			{
				m_isDirty = true;
			}
		}

		public void SetDirty()
		{
			m_isDirty = true;
		}

		public bool Equals(BaseStat x, BaseStat y) => x.GetHashCode() == y.GetHashCode();

		public override bool Equals(object obj)
		{
			return Equals((BaseStat) obj, this);
		}

		public override int GetHashCode() => GetType().Name.GetHashCode();

		public override string ToString()
		{
			var sign = AdditionalValue > 0 ? "+" : "-";
			if (m_modifiers.Count == 0)
			{
				return $"{BaseValue}";
			}

			return $"{BaseValue} {sign} {Mathf.Abs(AdditionalValue)}";
		}
	}
}
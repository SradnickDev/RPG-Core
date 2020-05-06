using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using RPGCore.Stat.Types;

namespace RPGCore.Stat
{
#region BsonSerialization

	[BsonDiscriminator(RootClass = true)]
	[BsonKnownTypes(typeof(Health), typeof(Strength),
					typeof(Mana), typeof(Intelligence), typeof(CritRate))]

#endregion

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
		private bool m_isDirty = false;

		private readonly List<Modifier> m_modifiers = new List<Modifier>();
		private float m_cachedValue;

		[BsonIgnore]
		public virtual float Min { get; set; } = 0;

		[BsonIgnore]
		public virtual float Max { get; set; } = 100;

		[BsonIgnore]
		public virtual float RoundTo { get; set; } = 1f;

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
						retVal += Value * (modifier.Value / 100f);
						break;
					case ModifierType.PercentOnCurrent:
						retVal *= 1 + (modifier.Value / 100f);
						break;
				}
			}

			m_cachedValue = retVal;
		}

		public void AddModifier(Modifier modifier)
		{
			m_isDirty = true;
			m_modifiers.Add(modifier);
			m_modifiers.Sort((a, b) => a.ModifierType.CompareTo(b.ModifierType));
		}

		public void RemoveModifer(Modifier modifier)
		{
			if (m_modifiers.Remove(modifier))
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
	}
}
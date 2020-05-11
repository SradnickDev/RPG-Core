using UnityEngine;

namespace RPGCore.Stat
{
	public class StatModifier
	{
		public ModifierType ModifierType = ModifierType.None;
		public float Value;
		public string Source;

		public StatModifier() { }

		public StatModifier(string source)
		{
			Source = source;
		}

		public StatModifier(ModifierType modifierType, float value, string source)
		{
			ModifierType = modifierType;
			Value = value;
			Source = source;
		}

		public override string ToString()
		{
			var sign = Value >= 0 ? "+" : "-";
			var perc = ModifierType == ModifierType.Constant ? "" : "%";
			var formattedVal = $"{sign} {Mathf.Abs(Value)} {perc} {Source}";
			return Value != 0 ? formattedVal : "";
		}
	}
}
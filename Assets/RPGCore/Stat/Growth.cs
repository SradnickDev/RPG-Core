using System.Collections.Generic;
using UnityEngine;

namespace RPGCore.Stat
{
	public class Growth
	{
		public Color Color;
		public AnimationCurve Curve = AnimationCurve.Linear(0, 0, 1, 1);
		public bool UseCurve = false;
		public List<float> CachedValues = new List<float>();
		public int MaxLevel;
		public float MinValue;
		public float MaxValue;
		public float DecimalTarget = 1;

		public float this[int position] =>
			position >= CachedValues.Count
				? CachedValues[CachedValues.Count - 1]
				: CachedValues[position];

		public Growth()
		{
			MaxLevel = 100;
			MinValue = 1;
			MaxValue = 100;
		}

		public Growth(int minVal, int maxVal, int maxLevel)
		{
			MaxLevel = maxLevel;
			MinValue = minVal;
			MaxValue = maxVal;
			Curve = AnimationCurve.Linear(0, minVal, maxLevel, maxVal);
		}
	}
}
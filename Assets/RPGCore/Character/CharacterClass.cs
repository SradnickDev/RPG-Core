using RPGCore.Stat;
using UnityEngine;

namespace RPGCore.Character
{
	public abstract class CharacterClass
	{
		public string Description;
		public Sprite Icon;
		public StatCollection Stats;

		public CharacterClass() { }

		public CharacterClass(StatCollection stats)
		{
			Stats = stats;
		}
	}

	public class Warrior : CharacterClass { }

	public class Mage : CharacterClass { }
}
namespace RPGCore.Stat.Types
{
	public class Mana : BaseStat
	{
		public Mana() { }

		public Mana(float amount)
		{
			BaseValue = amount;
		}

		public float Current;
	}
}
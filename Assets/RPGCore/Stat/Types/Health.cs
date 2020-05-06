namespace RPGCore.Stat.Types
{
	public class Health : BaseStat
	{
		public Health() { }

		public Health(float amount)
		{
			BaseValue = amount;
		}

		public float Current;
	}
}
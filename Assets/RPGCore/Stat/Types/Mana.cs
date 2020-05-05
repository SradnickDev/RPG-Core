namespace RPGCore.Stat.Types
{
	public class Mana : BaseStat
	{
		public override float Max { get; set; } = 1000;
		public Mana() { }
		public float Current;
	}
}
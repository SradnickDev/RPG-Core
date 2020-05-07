namespace RPGCore.Items.Types
{
	public class MiscItemDefinition : ItemDefinition
	{
		public MiscItemDefinition() : base() { }
		public MiscItemDefinition(MiscItemDefinition itemDefinition) : base(itemDefinition) { }

		public MiscItem Initialize()
		{
			return new MiscItem(this);
		}
	}
}
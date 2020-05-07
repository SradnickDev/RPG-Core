namespace RPGCore.Items.Types
{
	public class MiscItem : StackableItem
	{
		public override IItem Duplicate()
		{
			return new MiscItem((MiscItemDefinition) Definition);
		}

		public MiscItem(MiscItemDefinition itemDefinition)
		{
			Definition = new MiscItemDefinition(itemDefinition);
		}
	}
}
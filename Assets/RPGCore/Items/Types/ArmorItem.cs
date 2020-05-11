namespace RPGCore.Items.Types
{
	public class ArmorItem : UniqueItem
	{
		public ArmorDefinition ArmorDefinition => (ArmorDefinition)Definition;
		public ArmorItem(ArmorDefinition definition)
		{
			Definition = new ArmorDefinition(definition);
		}

		public override IItem Duplicate()
		{
			return new ArmorItem((ArmorDefinition) Definition);
		}
	}
}
namespace RPGCore.Items.Types
{
	public class WeaponItem : UniqueItem
	{
		public WeaponDefinition WeaponDefinitionDefinition => (WeaponDefinition) Definition;

		public WeaponItem(WeaponDefinition definition)
		{
			Definition = new WeaponDefinition(definition);
		}

		public override IItem Duplicate()
		{
			return new WeaponItem((WeaponDefinition) Definition);
		}
	}
}
using RPGCore.Items.Types.Templates;

namespace RPGCore.Items.Types
{
	public class ArmorItem : UniqueItem
	{
		public ArmorItem(ArmorItemTemplate itemTemplate)
		{
			ItemTemplate = new ArmorItemTemplate(itemTemplate);
		}

		public override IItem Duplicate()
		{
			return new ArmorItem((ArmorItemTemplate) ItemTemplate);
		}
	}
}
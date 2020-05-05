using RPGCore.Items.Types.Templates;

namespace RPGCore.Items.Types
{
	public class MiscItem : StackableItem
	{
		public override IItem Duplicate()
		{
			return new MiscItem((MiscItemTemplate) ItemTemplate);
		}

		public MiscItem(MiscItemTemplate itemTemplate)
		{
			ItemTemplate = new MiscItemTemplate(itemTemplate);
		}
	}
}
using RPGCore.Stat;

namespace RPGCore.Items.Types.Templates
{
	public class ArmorItemTemplate : ItemTemplate
	{
		public StatCollection Stats = new StatCollection();

		public ArmorItemTemplate() : base() { }

		public ArmorItemTemplate(ArmorItemTemplate itemTemplate) : base(itemTemplate) { }

		public ArmorItem Initialize()
		{
			return new ArmorItem(this);
		}
	}
}
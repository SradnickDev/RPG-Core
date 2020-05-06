using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using RPGCore.Items.Types.Templates;

namespace RPGCore.Items
{
#region BsonSerilizazion

	[BsonDiscriminator(RootClass = true)]
	[BsonKnownTypes(typeof(ConsumableItemTemplate), typeof(ArmorItemTemplate),
					typeof(WeaponItemTemplate), typeof(MiscItemTemplate))]

#endregion

	public abstract class ItemTemplate
	{
		public ObjectId Id;
		public string DisplayName;
		public string Description;
		public SpriteModel Icon = new SpriteModel();
		public Rarity Rarity = Rarity.Poor;

		public ItemTemplate()
		{
			Id = ObjectId.GenerateNewId();
		}

		public ItemTemplate(ItemTemplate itemTemplate)
		{
			Id = itemTemplate.Id;
			DisplayName = itemTemplate.DisplayName;
			Description = itemTemplate.Description;
			Icon = itemTemplate.Icon;
			Rarity = itemTemplate.Rarity;
		}
	}
}
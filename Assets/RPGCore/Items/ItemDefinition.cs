using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using RPGCore.Database.Item;
using RPGCore.Items.Types;

namespace RPGCore.Items
{
#region BsonSerilizazion

	[BsonDiscriminator(RootClass = true)]
	[BsonKnownTypes(typeof(ConsumableItemDefinition), typeof(ArmorDefinition),
					typeof(WeaponDefinition), typeof(MiscItemDefinition))]

#endregion

	public abstract class ItemDefinition
	{
		public ObjectId Id;
		public string DisplayName;
		public string Description;
		public SpriteModel Icon = new SpriteModel();
		public Rarity Rarity = Rarity.Poor;

		public ItemDefinition()
		{
			Id = ObjectId.GenerateNewId();
		}

		public ItemDefinition(ItemDefinition definition)
		{
			Id = definition.Id;
			DisplayName = definition.DisplayName;
			Description = definition.Description;
			Icon = definition.Icon;
			Rarity = definition.Rarity;
		}
	}
}
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using UnityEngine;

namespace RPGCore.Items
{
	[BsonDiscriminator(RootClass = true)]
	[BsonKnownTypes(typeof(ConsumableItemTemplate), typeof(ArmorItemTemplate),
					typeof(WeaponItemTemplate), typeof(MiscItemTemplate))]
	public abstract class ItemTemplate
	{
		public ObjectId Id;
		public string DisplayName;
		public string Description;
		public Sprite Icon;
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
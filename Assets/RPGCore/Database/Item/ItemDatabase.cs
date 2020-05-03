using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;
using RPGCore.Items;
using UnityEngine;

namespace RPGCore.Database.Item {
	public static class ItemDatabase
	{
		private static MongoDatabase<ItemTemplate> m_mongoDatabase;
		private static string m_connectionInfoPath = Application.dataPath + "/connectionInfo.json";

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		public static void Initialize()
		{
			m_mongoDatabase = new MongoDatabase<ItemTemplate>();
			var connectionInfo = DatabaseUtility.Load(m_connectionInfoPath);
			m_mongoDatabase.Connect(connectionInfo);
		}

		public static void InsertItem(ItemTemplate itemTemplate)
		{
			m_mongoDatabase.Insert(itemTemplate);
		}

		public static ItemTemplate Fetch(string name)
		{
			var filter = Builders<ItemTemplate>
						 .Filter
						 .Eq(nameof(ItemTemplate.DisplayName), name);

			var itemTemplates = m_mongoDatabase.Fetch(filter);
			return itemTemplates;
		}

		public static ItemTemplate Fetch(ObjectId id)
		{
			var filter = Builders<ItemTemplate>
						 .Filter
						 .Eq(nameof(ItemTemplate.Id), id);

			var itemTemplates = m_mongoDatabase.Fetch(filter);
			return itemTemplates;
		}

		public static IEnumerable<ItemTemplate> FetchAll()
		{
			var itemTemplates = m_mongoDatabase.FetchAll();
			return itemTemplates;
		}

		public static void Update(IEnumerable<ItemTemplate> items)
		{
			var models = items.Select(Selector);
			m_mongoDatabase.Update(models);
		}

		public static void Update(ItemTemplate item)
		{
			var filter = Builders<ItemTemplate>
						 .Filter
						 .Eq(nameof(ItemTemplate.Id), item.Id);

			m_mongoDatabase.Update(item, filter, true);
		}

		private static ReplaceOneModel<ItemTemplate> Selector(ItemTemplate item)
		{
			return new
				ReplaceOneModel<ItemTemplate>(new ExpressionFilterDefinition<ItemTemplate>(doc => doc.Id == item.Id),
											  item)
				{
					IsUpsert = true
				};
		}

		public static void DeleteById(ObjectId id)
		{
			var filter = Builders<ItemTemplate>
						 .Filter
						 .Eq(nameof(ItemTemplate.Id), id);
			m_mongoDatabase.Delete(filter);
		}
	}
}
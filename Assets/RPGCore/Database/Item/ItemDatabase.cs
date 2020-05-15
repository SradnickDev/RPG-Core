using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using RPGCore.Items;
using UnityEngine;

namespace RPGCore.Database.Item
{
	public static class ItemDatabase
	{
		private static MongoDatabase<ItemDefinition> m_mongoDatabase;
		private static string m_connectionInfoPath = Application.dataPath + "/connectionInfo.json";

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		public static void Initialize()
		{
			m_mongoDatabase = new MongoDatabase<ItemDefinition>();
			var connectionInfo = DatabaseUtility.Load(m_connectionInfoPath);
			m_mongoDatabase.Connect(connectionInfo);
		}

		public static void InsertItem(ItemDefinition itemDefinition)
		{
			m_mongoDatabase.Insert(itemDefinition);
		}

		public static ItemDefinition Fetch(string name)
		{
			var filter = Builders<ItemDefinition>
						 .Filter
						 .Eq(nameof(ItemDefinition.DisplayName), name);

			var itemDefinitions = m_mongoDatabase.Fetch(filter);
			return itemDefinitions;
		}

		public static ItemDefinition Fetch(ObjectId id)
		{
			var filter = Builders<ItemDefinition>
						 .Filter
						 .Eq(nameof(ItemDefinition.Id), id);

			var itemDefinitions = m_mongoDatabase.Fetch(filter);
			return itemDefinitions;
		}

		public static IEnumerable<ItemDefinition> FetchAll()
		{
			var itemDefinitions = m_mongoDatabase.FetchAll();
			return itemDefinitions;
		}

		public static void FetchAllASync(Action<int, int> progress,
										 Action<IEnumerable<ItemDefinition>> result,
										 CancellationToken token = default)
		{
			_ = FetchAllASyncInternal(progress, result, token);
		}

		private static async Task FetchAllASyncInternal(Action<int, int> progress,
														Action<IEnumerable<ItemDefinition>> result,
														CancellationToken token = default)
		{
			try
			{
				await m_mongoDatabase.FetchAllASync(progress, result, token);
			}
			catch (OperationCanceledException)
			{
				Debug.LogError("Fetching Items was cancelled.");
			}
		}

		public static void Update(IEnumerable<ItemDefinition> items)
		{
			var models = items.Select(Selector);
			m_mongoDatabase.Update(models);
		}

		public static void Update(ItemDefinition item)
		{
			var filter = Builders<ItemDefinition>
						 .Filter
						 .Eq(nameof(ItemDefinition.Id), item.Id);

			m_mongoDatabase.Update(item, filter, true);
		}

		private static ReplaceOneModel<ItemDefinition> Selector(ItemDefinition item)
		{
			return new
				ReplaceOneModel<ItemDefinition>(new ExpressionFilterDefinition<ItemDefinition>(doc => doc.Id == item.Id),
												item)
				{
					IsUpsert = true
				};
		}

		public static void DeleteById(ObjectId id)
		{
			var filter = Builders<ItemDefinition>
						 .Filter
						 .Eq(nameof(ItemDefinition.Id), id);
			m_mongoDatabase.Delete(filter);
		}
	}
}
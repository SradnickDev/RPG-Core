using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using RPGCore.Database.Mongo;

namespace RPGCore.Database
{
	public class MongoDatabase<T> : IDisposable
	{
		private MongoClient m_client;
		private IMongoDatabase m_database;
		private IMongoCollection<T> m_collection;
		private ConnectionInfo m_info;

		public void Connect(ConnectionInfo info)
		{
			BsonSerializer.RegisterSerializationProvider(new UnityResolver());

			m_info = info;
			m_client = new MongoClient(info.DatabaseAddress);
			m_database = m_client.GetDatabase(info.DatabaseName);
			m_collection = m_database.GetCollection<T>(info.CollectionName);
		}

#region Fetch

		public T Fetch(FilterDefinition<T> filter)
		{
			var result = m_collection.Find(filter).Single();
			return result;
		}

		public IEnumerable<T> FetchAll()
		{
			return m_collection.Find(FilterDefinition<T>.Empty).ToEnumerable();
		}

		public async Task FetchAllASync(Action<int, int> progress,
										Action<IEnumerable<T>> result,
										CancellationToken token = default)
		{
			var count =
				await m_collection.CountDocumentsAsync(FilterDefinition<T>.Empty, null, token);
			var cursor = await m_collection.FindAsync(FilterDefinition<T>.Empty, null, token);
			var retVal = new List<T>();

			token.ThrowIfCancellationRequested();

			while (await cursor.MoveNextAsync())
			{
				token.ThrowIfCancellationRequested();
				var entry = cursor.Current;
				foreach (var e in entry)
				{
					token.ThrowIfCancellationRequested();
					retVal.Add(e);
					progress(retVal.Count, (int) count);
				}
			}

			result(retVal);
		}

#endregion

#region Insert

		public void Insert(T objectToInsert)
		{
			m_collection.InsertOne(objectToInsert);
		}

		public void Insert(IEnumerable<T> objectsToInsert)
		{
			m_collection.InsertMany(objectsToInsert);
		}

#endregion

#region Update

		public void Update(IEnumerable<ReplaceOneModel<T>> models)
		{
			m_collection.BulkWrite(models);
		}

		public void Update(T objectToUpdate, FilterDefinition<T> filter, bool isUpsert)
		{
			m_collection.ReplaceOne(filter, objectToUpdate,
									new UpdateOptions() {IsUpsert = isUpsert});
		}

#endregion

#region Delete

		public void Delete(FilterDefinition<T> filter)
		{
			m_collection.DeleteOne(filter);
		}

#endregion

		public void Dispose()
		{
			m_client = null;
			m_database = null;
			m_collection = null;
		}
	}
}
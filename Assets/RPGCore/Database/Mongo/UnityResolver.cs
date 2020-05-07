using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization;
using RPGCore.Database.Item;

namespace RPGCore.Database.Mongo
{
	internal sealed class UnityResolver : IBsonSerializationProvider
	{
		public IBsonSerializer GetSerializer(Type type)
		{
			return UnityFormatterMap.GetFormatter(type);
		}

		internal static class UnityFormatterMap
		{
			private static readonly Dictionary<Type, IBsonSerializer> FormatterMap =
				new Dictionary<Type, IBsonSerializer>()
				{
					{typeof(SpriteModel), new SpriteModelFormatter()},
					{typeof(AudioClipModel), new AudioClipModelFormatter()},
				};

			internal static IBsonSerializer GetFormatter(Type t)
			{
				if (FormatterMap.TryGetValue(t, out var formatter))
				{
					return formatter;
				}

				return null;
			}
		}
	}
}
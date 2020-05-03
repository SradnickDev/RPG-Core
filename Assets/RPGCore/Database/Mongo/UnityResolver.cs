using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization;
using UnityEngine;

namespace RPGCore.Database.Mongo
{
	public sealed class UnityResolver : IBsonSerializationProvider
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
					{typeof(Sprite), new SpriteFormatter()},

					//{typeof(AudioClip), new AudioClipFormatter()},
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
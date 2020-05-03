using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using RPGCore.AssetBundle;
using UnityEngine;

namespace RPGCore.Database.Mongo
{
	internal sealed class SpriteFormatter : SerializerBase<Sprite>
	{
		public override Sprite Deserialize(BsonDeserializationContext context,
										   BsonDeserializationArgs args)
		{
			//context should contain asset name and bundle name
			var content = context.Reader.ReadString();

			//otherwise stop here
			if (string.IsNullOrEmpty(content))
			{
				return null;
			}

			var assetName = AssetBundleUtility.GetAssetName(content);
			var bundle = AssetBundleUtility.GetBundle(content);

		#if UNITY_EDITOR

			//while not in play mode, means using the item editor
			//load the asset from AssetDatabase
			if (Application.isEditor && !Application.isPlaying)
			{
				return AssetBundleUtility.AssetFromAssetDatabase<Sprite>(bundle, assetName);
			}
		#endif

			return AssetBundleHandle.LoadAsset<Sprite>(bundle, assetName);
		}

		public override void Serialize(BsonSerializationContext context,
									   BsonSerializationArgs args,
									   Sprite value)
		{
			//only execute in editor mode and while application is not playing
		#if UNITY_EDITOR
			if (Application.isEditor && !Application.isPlaying)
			{
				var name = AssetBundleUtility.CreateAssetName(value);
				context.Writer.WriteString(name);
			}
			else
			{
				Debug.LogError("Cant Serialize Assets at runtime. Leave Play Mode!");
			}
		#endif
		}
	}
}
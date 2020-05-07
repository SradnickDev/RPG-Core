using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using RPGCore.Database.Item;

namespace RPGCore.Database.Mongo
{
	internal sealed class SpriteModelFormatter : SerializerBase<SpriteModel>
	{
		public override SpriteModel Deserialize(BsonDeserializationContext context,
												BsonDeserializationArgs args)
		{
			var content = context.Reader.ReadString();
			return new SpriteModel(content);
		}

		public override void Serialize(BsonSerializationContext context,
									   BsonSerializationArgs args,
									   SpriteModel value)
		{
			var modelReference = value.Save();
			context.Writer.WriteString(modelReference);
		}
	}
	
	internal sealed class AudioClipModelFormatter : SerializerBase<AudioClipModel>
	{
		public override AudioClipModel Deserialize(BsonDeserializationContext context,
												   BsonDeserializationArgs args)
		{
			var content = context.Reader.ReadString();
			return new AudioClipModel(content);
		}

		public override void Serialize(BsonSerializationContext context,
									   BsonSerializationArgs args,
									   AudioClipModel value)
		{
			var modelReference = value.Save();
			context.Writer.WriteString(modelReference);
		}
	}
}
using UnityEngine;

namespace RPGCore.Database.Item
{
	public class SpriteModel : DataModel<Sprite>
	{
		public SpriteModel() { }
		public SpriteModel(string reference) : base(reference) { }
		public SpriteModel(Sprite data) : base(data) { }
	}
}
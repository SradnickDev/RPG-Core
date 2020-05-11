using UnityEngine;

namespace RPGCore.Database.Item
{
	public class AudioClipModel : DataModel<AudioClip>
	{
		public AudioClipModel() { }
		public AudioClipModel(string reference) : base(reference) { }
		public AudioClipModel(AudioClip data) : base(data) { }
	}
}
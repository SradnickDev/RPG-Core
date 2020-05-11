using UnityEngine;

namespace RPGCore.Database.Item
{
	public class AnimOverrideControllerModel : DataModel<AnimatorOverrideController>
	{
		public AnimOverrideControllerModel() { }
		public AnimOverrideControllerModel(string reference) : base(reference) { }
		public AnimOverrideControllerModel(AnimatorOverrideController data) : base(data) { }
	}
}
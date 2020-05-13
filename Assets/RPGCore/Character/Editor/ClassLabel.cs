using RPGCore.Editor;
using UnityEngine;

namespace RPGCore.Character.Editor
{
	internal class ClassLabel : ListLabel
	{
		public string ClassName => Source.GetType().Name;
		public CharacterClass Source;

		public ClassLabel(CharacterClass characterSource) : base()
		{
			Set(characterSource);
		}

		public void Set(CharacterClass characterClass)
		{
			Source = characterClass;
			Set(ClassName
			  , ""
			  , Source.Icon?.texture
			  , Color.black);
		}

		public override void Draw()
		{
			if (Source == null)
			{
				Debug.LogError("Class label , CharacterClass is null!");
				return;
			}

			UpdateLabelInformation();
			base.Draw();
		}
		
		private void UpdateLabelInformation() => Set(Source);
	}
}
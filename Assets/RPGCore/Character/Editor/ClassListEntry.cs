using RPGCore.Editor;
using UnityEngine;

namespace RPGCore.Character.Editor
{
	internal class ClassListEntry : ListEntry
	{
		public string ClassName => Source.GetType().Name;
		public CharacterClass Source;

		public ClassListEntry(CharacterClass characterSource)
		{
			Set(characterSource);
		}

		public void Set(CharacterClass characterClass)
		{
			Source = characterClass;
		}

		private void UpdateLabelInformation()
		{
			DrawLabel(ClassName
					, ""
					, Source.Icon?.texture
					, Color.black);
		}

		public override void Draw()
		{
			if (Source == null)
			{
				Debug.LogError("CharacterClass Entry , CharacterClass is null!");
				return;
			}

			UpdateLabelInformation();
		}
	}
}
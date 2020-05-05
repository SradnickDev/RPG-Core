using System;
using UnityEngine;

namespace RPGCore.Items.Editor
{
	public class FieldAction
	{
		public string Name;

		public FieldAction(string name)
		{
			Name = name;
		}
	}

	public class ButtonAction : FieldAction
	{
		public readonly Action Action;
		public readonly GUIContent GuiContent;

		public ButtonAction(string name, Action action) : base(name)
		{
			Action = action;
		}

		public ButtonAction(string name, Texture icon, Action action) : base(name)
		{
			Action = action;
			GuiContent = new GUIContent(name, icon);
		}
	}

	public class InputAction : FieldAction
	{
		public string Input;
		public InputAction(string name) : base(name) { }
	}

	public class SpaceAction : FieldAction
	{
		public SpaceAction() : base("") { }

		public void Draw()
		{
			GUILayout.FlexibleSpace();
		}
	}
}
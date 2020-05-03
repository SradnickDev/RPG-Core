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

		public ButtonAction(string name, Action action) : base(name)
		{
			Action = action;
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
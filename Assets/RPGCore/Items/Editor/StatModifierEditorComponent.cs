using System.Collections.Generic;
using RPGCore.Items.Types;
using RPGCore.Stat;
using UnityEditor;
using UnityEngine;

namespace RPGCore.Items.Editor
{
	internal class StatModifierEditorComponent : EditorComponent<ItemDefinition>
	{
		public StatModifierEditorComponent() { }

		public override void Draw()
		{
			if (Source == null) return;

			//TODO Add Error message if Definition is null

			DrawStats();
		}

		private void DrawStats()
		{
			GUILayout.Space(20);

			GUILayout.BeginVertical();
			List<StatModifier> stats = null;
			if (Source.GetType() == typeof(ArmorDefinition))
			{
				stats = ((ArmorDefinition) Source).Stats;
			}

			if (Source.GetType() == typeof(WeaponDefinition))
			{
				stats = ((WeaponDefinition) Source).Stats;
			}

			for (var i = 0; i < stats.Count; i++)
			{
				var stat = stats[i];
				EditorExtension.DrawStatModifier(stat, 0, 100, 1,
												 s => stats.Remove(s));
			}

			GUILayout.EndVertical();

			GUILayout.FlexibleSpace();
			GUILayout.BeginHorizontal(EditorStyles.toolbar,
									  GUILayout.ExpandWidth(true),
									  GUILayout.MaxHeight(22));


			var addIcon = EditorGUIUtility.IconContent("Toolbar Plus More");
			EditorExtension.ClassDropDown<BaseStat>(new GUIContent(addIcon)
												  , t =>
													{
														t.BaseValue = 20;
														stats?.Add(new StatModifier(t.GetType()
																					 .Name));
													}, EditorStyles.toolbarButton);

			if (GUILayout.Button(EditorGUIUtility.IconContent("TreeEditor.Trash"),
								 EditorStyles.toolbarButton))
			{
				stats.Clear();
			}

			GUILayout.EndHorizontal();
		}
	}
}
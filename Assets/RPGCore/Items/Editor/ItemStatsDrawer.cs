using RPGCore.Items.Types;
using RPGCore.Stat;
using UnityEditor;
using UnityEngine;

namespace RPGCore.Items.Editor
{
	internal class ItemStatsDrawer : ItemBodyDrawer
	{
		public override void Draw(EditorWindow editorWindow)
		{
			if (Definition == null) return;

			//TODO Add Error message if Definition is null

			DrawStats();
		}

		private void DrawStats()
		{
			GUILayout.Space(20);

			GUILayout.BeginVertical();
			StatCollection stats = null;
			if (Definition.GetType() == typeof(ArmorDefinition))
			{
				stats = ((ArmorDefinition) Definition).Stats;
			}

			if (Definition.GetType() == typeof(WeaponDefinition))
			{
				stats = ((WeaponDefinition) Definition).Stats;
			}

			for (var i = 0; i < stats.Count; i++)
			{
				var stat = stats[i];
				EditorExtension.DrawStat(stat, s => stats.Remove(s));
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
														stats?.Add(t);
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
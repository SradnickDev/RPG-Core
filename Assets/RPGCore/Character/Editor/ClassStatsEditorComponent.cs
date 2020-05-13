using RPGCore.Items.Editor;
using RPGCore.Stat;
using RPGCore.Stat.Types;
using UnityEditor;
using UnityEngine;

namespace RPGCore.Character.Editor
{
	internal class ClassStatsEditorComponent : EditorComponent<CharacterClass>
	{
		private StatCollection m_stats = new StatCollection();
		private StatDrawer m_statDrawer;
		Health m_health = new Health();
		private Vector2 m_scrollPosition = new Vector2();

		public ClassStatsEditorComponent()
		{
			m_statDrawer = new StatDrawer();
			m_statDrawer.Source = m_health;
		}

		public override void Draw()
		{
			if (Source == null) return;
			m_statDrawer.Window = Window;

			//TODO Add Error message if source is null

			DrawStats();
		}

		private void DrawStats()
		{
			m_scrollPosition = EditorGUILayout.BeginScrollView(m_scrollPosition, false, false);
			GUILayout.Space(20);
			GUILayout.BeginVertical();
			m_statDrawer.Draw();
			m_statDrawer.Draw();
			m_statDrawer.Draw();
			m_statDrawer.Draw();
			m_statDrawer.Draw();
			m_statDrawer.Draw();
			EditorGUILayout.EndScrollView();
			GUILayout.Space(20);
			GUILayout.EndHorizontal();
		}
	}
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using RPGCore.Items.Editor;
using RPGCore.Stat;
using UnityEditor;
using UnityEngine;

namespace RPGCore.Character.Editor
{
	internal class StatDrawer : EditorComponent<BaseStat>
	{
		public string ClassName;
		private GUIStyle m_editorStyle;
		private Color m_color = new Color(0.25f, 0.54f, 1f);

		public StatDrawer() { }

		public override void Draw()
		{
			if (Source == null) return;

			m_editorStyle = new GUIStyle()
				.NormalBackground(new Texture2D(1, 1));

			GUILayout.BeginVertical(EditorStyles.helpBox);
			DrawOptions();

			GUILayout.BeginHorizontal(EditorStyles.helpBox);

			GUILayout.Space(4);
			DrawContentBox(Source.Growth);
			GUILayout.Space(4);
			GUILayout.EndHorizontal();

			DrawSettings(Source.Growth);
			GUILayout.EndVertical();
		}

		private void DrawOptions()
		{
			GUILayout.BeginHorizontal();

			GUILayout.Label(Source.GetType().Name, EditorStyles.largeLabel);
			GUILayout.FlexibleSpace();
			if (EditorGUILayout.DropdownButton(new GUIContent("Options"), FocusType.Keyboard,
											   GUILayout.MaxWidth(88)))
			{
				var menu = new GenericMenu();

				menu.AddItem(new GUIContent("Import CSV"), false, () =>
				{
					var path = EditorUtility.OpenFilePanel("Overwrite with CSV", "", "csv");

					Source.Growth.CachedValues = new List<float>();

					foreach (var line in File.ReadLines(path))
					{
						if (float.TryParse(line, out var result))
						{
							Source.Growth.CachedValues.Add(result);
						}
					}

					Source.Growth.MaxLevel = Source.Growth.CachedValues.Count;
					Source.Growth.MinValue = Source.Growth.CachedValues.Min();
					Source.Growth.MaxValue = Source.Growth.CachedValues.Max();
				});

				menu.AddItem(new GUIContent("Export CSV"), false, () =>
				{
					var path = EditorUtility
						.SaveFilePanel(
									   "Save as CSV",
									   "",
									   $"{ClassName}_{Source.GetType().Name}_growth.csv",
									   "csv");

					if (path.Length != 0)
					{
						var content = new string[Source.Growth.CachedValues.Count];

						for (var i = 0; i < Source.Growth.CachedValues.Count; i++)
						{
							content[i] = Source
										 .Growth.CachedValues[i]
										 .ToString();
						}

						File.WriteAllLines(path, content);
					}
				});

				menu.AddItem(new GUIContent("Reset"), false, () =>
				{
					Source.Growth.Curve = AnimationCurve.Linear(0, 0, 1, 1);
					Source.Growth.MaxLevel = 60;
					Source.Growth.MinValue = 0;
					Source.Growth.MaxValue = 100;
					Source.Growth.CachedValues = new List<float>();
				});

				menu.ShowAsContext();
			}

			GUILayout.EndHorizontal();
		}

		private void DrawContentBox(Growth growth)
		{
			var bar = EditorGUILayout.GetControlRect(GUILayout.MinHeight(80),
													 GUILayout.MaxHeight(150));
			var areaRect = bar;

			var array = growth.CachedValues.ToArray();
			Array.Resize(ref array, growth.MaxLevel);

			growth.CachedValues = array.ToList();
			if (growth.CachedValues.Count < growth.MaxLevel)
			{
				growth.CachedValues.Capacity = growth.MaxLevel;
			}

			var lightColor = m_color;
			lightColor.a = 0.1f;

			GUI.color = lightColor;
			GUI.Box(bar, "", m_editorStyle);

			var selection = DrawBarByMouse(growth, areaRect, out var value);

			SetByCurve(growth);

			ApplyValueToBar(growth, selection, value);


			bar.y += bar.height;
			bar.width /= growth.MaxLevel;
			bar.width -= 1;

			lightColor.a = 0.5f;
			GUI.color = lightColor;

			for (var i = 0; i < growth.MaxLevel; i++)
			{
				var p = Mathf.InverseLerp(growth.MinValue, growth.MaxValue,
										  growth.CachedValues[i]);
				bar.height = Mathf.Lerp(0, -areaRect.height, p);

				var color = GUI.color;


				if (growth.CachedValues.Count >= i)
				{
					if (selection >= 0 && selection == i)
					{
						GUI.color += new Color(0, 0, 0, 1);
					}


					GUI.Box(bar, "", m_editorStyle);

					GUI.color = color;
					GUI.color += new Color(0.4f, 0.4f, 0.4f, 0f);
					if (selection >= 0 && selection == i)
					{
						GUI.color += new Color(0, 0, 0, 1);
					}

					var style = new GUIStyle()
								.FontStyle(FontStyle.Bold)
								.FontSize(6);

					GUI.Label(new Rect(bar.x, bar.y - 14, 5, 30), growth.CachedValues[i] + "",
							  style);
				}

				bar.x += bar.width + 1;

				Window.Repaint();
				GUI.color = color;
			}


			var rect = areaRect;

			rect.y += 2;
			rect.x += 10;

			if (selection >= 0)
			{
				GUI.Label(rect, $"Level: {(selection + 1)}", RpgEditorStyles.TitleStyle);
			}

			rect.y += 12;

			if (selection >= 0)
			{
				GUI.Label(rect,
						  $"Amount: {growth.CachedValues[selection]}", RpgEditorStyles.TitleStyle);
			}


			GUI.color = Color.white;
		}

		private void ApplyValueToBar(Growth growth, int selection, float value)
		{
			if (Event.current != null)
			{
				switch (Event.current.rawType)
				{
					case EventType.MouseDrag:
						if (selection >= 0)
						{
							growth.CachedValues[selection] = value;
						}

						Window.Repaint();
						break;
				}
			}
		}

		private void SetByCurve(Growth growth)
		{
			if (growth.UseCurve)
			{
				for (int i = 0; i < growth.MaxLevel; i++)
				{
					var time = Mathf.InverseLerp(0, growth.MaxLevel - 1, i);
					var amount = Mathf.Lerp(growth.MinValue, growth.MaxValue,
											growth.Curve.Evaluate(time));
					amount = RoundDecimal(amount);
					growth.CachedValues[i] = amount;
				}
			}
		}

		private int DrawBarByMouse(Growth growth, Rect areaRect, out float value)
		{
			var selection = -1;
			value = 0f;

			var mousePos = new Vector2(Event.current.mousePosition.x - areaRect.x,
									   Event.current.mousePosition.y - areaRect.y);

			if (areaRect.Contains(Event.current.mousePosition))
			{
				var xPerc = Mathf.InverseLerp(0, areaRect.width, mousePos.x);
				var yPerc = Mathf.InverseLerp(0, areaRect.height, mousePos.y);

				selection = Mathf.FloorToInt(Mathf.Lerp(0, growth.MaxLevel, xPerc));

				value = Mathf.Lerp(growth.MaxValue, growth.MinValue, yPerc);
				value = RoundDecimal(value);
				if (Event.current.button == 1)
				{
					var menu = new GenericMenu();
					menu.AddItem(new GUIContent("+1"), false,
								 () => growth.CachedValues[selection]++);
					menu.AddItem(new GUIContent("-1"), false,
								 () => growth.CachedValues[selection]--);
					menu.AddItem(new GUIContent("min"), false,
								 () => growth.CachedValues[selection] =
									 growth.MinValue);
					menu.AddItem(new GUIContent("max"), false,
								 () => growth.CachedValues[selection] =
									 growth.MaxValue);
					menu.ShowAsContext();
				}
			}

			return selection;
		}

		public float RoundDecimal(float value)
		{
			return Mathf.Round(value * Source.Growth.DecimalTarget) / Source.Growth.DecimalTarget;
		}

		private void DrawSettings(Growth growth)
		{
			GUILayout.BeginHorizontal();

			GUILayout.Label("Settings :");

			GUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("Max Level", GUILayout.Width(60));
			growth.MaxLevel = EditorGUILayout.IntField(growth.MaxLevel, GUILayout.Width(50));
			GUILayout.EndHorizontal();

			GUILayout.Space(5);

			GUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("Amount Range", GUILayout.Width(90));
			growth.MinValue =
				EditorGUILayout.FloatField(growth.MinValue, GUILayout.Width(50));
			growth.MaxValue =
				EditorGUILayout.FloatField(growth.MaxValue, GUILayout.Width(50));
			GUILayout.EndHorizontal();

			GUILayout.Space(5);

			GUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("Use Curve", GUILayout.Width(88));
			growth.UseCurve = EditorGUILayout.Toggle("", growth.UseCurve, GUILayout.Width(50));
			if (!growth.UseCurve) GUI.enabled = false;
			growth.Curve = EditorGUILayout.CurveField(growth.Curve, GUILayout.Width(80));
			GUI.enabled = true;
			GUILayout.EndHorizontal();

			GUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("Decimal", GUILayout.Width(50));
			growth.DecimalTarget =
				EditorGUILayout.FloatField(growth.DecimalTarget, GUILayout.Width(50));
			GUILayout.EndHorizontal();

			GUILayout.EndHorizontal();
		}
	}
}
using System.Text;
using RPGCore.Stat;
using RPGCore.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

#pragma warning disable 0649
namespace RPGCore.Inventory
{
	public class CharacterStatsView : MonoBehaviour
	{
		[SerializeField] private GraphicRaycaster m_raycaster;
		[SerializeField] private TextMeshProUGUI m_statsLabel;
		[SerializeField] private StatsDetailView m_statsDetailView;
		private bool m_isHovering = false;
		private int m_selectedLink = -1;
		private StatCollection m_stats;
		private StringBuilder m_stringBuilder = new StringBuilder();
		private GraphicHoverTrigger m_hoverTrigger;

		private void Start()
		{
			m_hoverTrigger = new GraphicHoverTrigger(m_raycaster, OnPointerEnter, OnPointerExit);
		}

		public void Set(StatCollection stats)
		{
			m_stats = stats;
		}

		private void Update()
		{
			if (m_stats == null) return;

			UpdateStats();
			m_hoverTrigger.Update();
			CheckStatIntersection();
		}

		private void UpdateStats()
		{
			foreach (var stat in m_stats)
			{
				FormatStat(stat);
			}

			if (!string.Equals(m_statsLabel.text, m_stringBuilder.ToString()))
			{
				m_statsLabel.text = m_stringBuilder.ToString();
			}

			m_stringBuilder.Clear();
		}

		private void FormatStat(BaseStat stat)
		{
			var statColor = Color.white;

			if (stat.Value > stat.BaseValue) statColor = Color.green;
			if (stat.Value < stat.BaseValue) statColor = Color.red;
			var colorCode = ColorUtility.ToHtmlStringRGB(statColor);

			m_stringBuilder
				.Append($"<link=\"{stat.ToString()}\">{stat.GetType().Name} : <color=#{colorCode}>{stat.Value}</color></link>");
			m_stringBuilder.Append("\n");
		}

		private void CheckStatIntersection()
		{
			if (m_isHovering)
			{
				// Check if mouse intersects with any links.
				var linkIndex =
					TMP_TextUtilities.FindIntersectingLink(m_statsLabel, Input.mousePosition, null);

				// Clear previous link selection if one existed.
				if ((linkIndex == -1 && m_selectedLink != -1) || linkIndex != m_selectedLink)
				{
					m_statsDetailView.Hide();
					m_selectedLink = -1;
				}

				// Handle new Link selection.
				if (linkIndex != -1 && linkIndex != m_selectedLink)
				{
					m_selectedLink = linkIndex;

					var linkInfo = m_statsLabel.textInfo.linkInfo[linkIndex];

					m_statsDetailView.Show(linkInfo.GetLinkID());
					m_statsDetailView.transform.position = Input.mousePosition;
				}
			}
		}

		private void OnPointerEnter(GameObject obj)
		{
			if (obj == m_statsLabel.gameObject)
			{
				m_isHovering = true;
			}
		}

		private void OnPointerExit(GameObject obj)
		{
			m_isHovering = false;
			m_statsDetailView.Hide();
		}
	}
}
#pragma warning restore 0649
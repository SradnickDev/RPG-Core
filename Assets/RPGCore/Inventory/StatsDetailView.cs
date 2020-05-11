using TMPro;
using UnityEngine;

namespace RPGCore.Inventory
{
	public class StatsDetailView : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI m_detailLabel;

		public void Show(string content)
		{
			m_detailLabel.text = content;
			this.gameObject.SetActive(true);
		}

		public void Hide()
		{
			this.gameObject.SetActive(false);
		}
	}
}
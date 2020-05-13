using TMPro;
using UnityEngine;

#pragma warning disable 0649
namespace RPGCore.Inventory
{
	public class StatsDetailView : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI m_detailLabel;

		public void Show(string content)
		{
			m_detailLabel.text = content;
			gameObject.SetActive(true);
		}

		public void Hide()
		{
			gameObject.SetActive(false);
		}
	}
}
#pragma warning restore 0649
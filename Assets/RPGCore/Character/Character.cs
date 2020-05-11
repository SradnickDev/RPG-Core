using RPGCore.Inventory;
using RPGCore.Stat;
using RPGCore.Stat.Types;
using UnityEngine;

#pragma warning disable 0649
public class Character : MonoBehaviour
{
	[SerializeField] private Inventory m_inventory;
	[SerializeField] private Equipment m_equipment;
	[SerializeField] private Weapons m_weapons;
	[SerializeField] private CharacterStatsView m_statsView;
	public StatCollection Stats => m_stats;
	private StatCollection m_stats = new StatCollection();

	private void Start()
	{
		m_stats.Add(new Health(100));
		m_inventory.Setup(this);
		m_equipment.Setup(this);
		m_weapons.Setup(this);
		m_statsView.Set(m_stats);
	}
}
#pragma warning restore 0649
using RPGCore.Inventory;
using RPGCore.Stat;
using RPGCore.Stat.Types;
using UnityEngine;

#pragma warning disable 0649
namespace RPGCore.Character
{
	public class Character : MonoBehaviour
	{
		[SerializeField] private Inventory.Inventory m_inventory;
		[SerializeField] private Equipment m_equipment;
		[SerializeField] private Weapons m_weapons;
		[SerializeField] private CharacterStatsView m_statsView;
		public StatCollection Stats = new StatCollection();

		private void Start()
		{
			Stats.Add(new Health(10));
			Stats.Add(new Mana(10));
			Stats.Add(new Strength(10));
			Stats.Add(new Intelligence(10));

			m_inventory.Setup(this);
			m_equipment.Setup(this);
			m_weapons.Setup(this);
			m_statsView.Set(Stats);
		}
	}
}
#pragma warning restore 0649
using System.Collections.Generic;
using MongoDB.Bson;
using RPGCore.Database.Item;
using RPGCore.Inventory.Slots;
using RPGCore.Items;
using RPGCore.Items.Types;
using RPGCore.Stat;
using RPGCore.Utilities;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

#pragma warning disable 0649
namespace RPGCore.Inventory
{
	public class CharacterStatsView : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI m_statsLabel;

		public void Set(StatCollection stats) { }
	}

	public class Inventory : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
	{
		public UnityEvent DragItem;
		public UnityEvent DropItem;

		public Equipment Equipment;
		public Weapons Weapons;
		[SerializeField] private List<SlotCollection> m_slotCollections;
		[SerializeField] private Image m_dragableIcon;

		private BaseSlot this[int bagId, int slotIdx] => m_slotCollections[bagId][slotIdx];
		private BaseSlot m_selectedSlot;
		private IItem m_selectedItem;
		private Character m_owner;

		private void Start()
		{
			AssignId();
			DebugStuff();
		}

		public void Setup(Character owner)
		{
			m_owner = owner;
		}

		private void DebugStuff()
		{
			var tx2d = EditorGUIUtility.FindTexture("console.erroricon");
			var debugSprite = Sprite.Create(tx2d, new Rect(0, 0, tx2d.width, tx2d.height),
											new Vector2(0.5f, 0.5f), 100f);
			var debugSprite2 = Sprite.Create(tx2d, new Rect(0, 0, tx2d.width, tx2d.height),
											 new Vector2(0.5f, 0.5f), 100f);
			var def = new ArmorDefinition()
			{
				ArmorType = ArmorType.Chest,
				DisplayName = "Chest",
				Description = "debug Chest",
				Icon = new SpriteModel(debugSprite),
				Id = ObjectId.GenerateNewId(),
				Rarity = Rarity.Common
			};

			var weadef = new WeaponDefinition()
			{
				WeaponType = WeaponType.MainHand,
				DisplayName = "MainHand",
				Description = "MainHand debug",
				Icon = new SpriteModel(debugSprite2),
				Id = ObjectId.GenerateNewId(),
				Rarity = Rarity.Legendary
			};

			var codef = new ConsumableItemDefinition()
			{
				DisplayName = "Consumable",
				Description = "Consumable",
				Icon = new SpriteModel(debugSprite2),
				Id = ObjectId.GenerateNewId(),
				Rarity = Rarity.Legendary
			};

			Weapons[WeaponType.MainHand].Add(new WeaponItem(weadef));
			Equipment[ArmorType.Chest].Add(new ArmorItem(def));
			this[0, 0].Add(new ConsumableItem(codef));
		}

		public void AssignId()
		{
			for (var i = 0; i < m_slotCollections.Count; i++)
			{
				m_slotCollections[i].Id = i;
			}
		}

		public void OnBeginDrag(PointerEventData eventData) => CheckDragableItem(eventData);

		private void CheckDragableItem(PointerEventData eventData)
		{
			if (ItemUtilities.TryGetSlot(eventData.pointerEnter, out var targetSlot))
			{
				if (!targetSlot.IsEmpty)
				{
					DragItem?.Invoke();
					m_selectedSlot = targetSlot;
					m_selectedItem = targetSlot.Content;
					m_selectedSlot.Remove();
					EnableDragableIcon(eventData);
				}
			}
		}

		public void OnDrag(PointerEventData eventData) => UpdateDragableIcon(eventData);

		private void UpdateDragableIcon(PointerEventData eventData)
		{
			if (!m_selectedSlot) return;
			m_dragableIcon.transform.position += (Vector3) eventData.delta;
		}

		public void OnEndDrag(PointerEventData eventData) => OnDrop(eventData);

		private void OnDrop(PointerEventData eventData)
		{
			if (m_selectedItem == null || m_selectedSlot == null) return;

			DropItem?.Invoke();

			if (ItemUtilities.TryGetSlot(eventData.pointerEnter, out var targetSlot))
			{
				if (targetSlot.IsEmpty && targetSlot.CanDropItem(m_selectedItem))
				{
					//just add item to an empty slot
					targetSlot.Add(m_selectedItem);
				}
				else if (targetSlot.IsStackableWith(m_selectedItem))
				{
					if (targetSlot.TryStackItem(m_selectedItem))
					{
						//if stacking where successful, reset and stop here
						ResetSelection();
						return;
					}

					//targetSlot / item has already maximum stacks
					//add selected item back to selected slot
					m_selectedSlot.Add(m_selectedItem);
				}
				else
				{
					if (targetSlot.CanDropItem(m_selectedItem))
					{
						//switch items
						var otherItem = targetSlot.Content;
						targetSlot.Remove();
						m_selectedSlot.Add(otherItem);
						targetSlot.Add(m_selectedItem);
					}
					else
					{
						m_selectedSlot.Add(m_selectedItem);
					}
				}
			}
			else
			{
				//reset to origin, no slot available
				m_selectedSlot.Add(m_selectedItem);
			}

			//in any case reset selected slot, selected item and disable icon
			ResetSelection();
		}

		private void EnableDragableIcon(PointerEventData eventData)
		{
			m_dragableIcon.transform.SetAsLastSibling();
			m_dragableIcon.gameObject.SetActive(true);
			m_dragableIcon.sprite = m_selectedItem.Definition.Icon.Data;
			m_dragableIcon.transform.position = eventData.position;
		}

		private void DisableDragableIcon()
		{
			m_dragableIcon.gameObject.SetActive(false);
			m_dragableIcon.sprite = null;
		}

		private void ResetSelection()
		{
			DisableDragableIcon();
			m_selectedItem = null;
			m_selectedSlot = null;
		}
	}
}
#pragma warning restore 0649
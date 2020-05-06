using System.Collections.Generic;
using MongoDB.Bson;
using RPGCore.Inventory.Slots;
using RPGCore.Items;
using RPGCore.Items.Types;
using RPGCore.Items.Types.Templates;
using RPGCore.Stat;
using RPGCore.Stat.Types;
using RPGCore.Utilities;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

#pragma warning disable 0649
namespace RPGCore.Inventory
{
	public class Inventory : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
	{
		[SerializeField] private UnityEvent DragItem;
		[SerializeField] private UnityEvent DropItem;
		[SerializeField] private List<Bag> m_bags;
		[SerializeField] private Image m_dragableIcon = null;

		[Header("Debug")]
		[SerializeField] private Sprite m_debugSprite;

		private ItemSlot this[int bagId, int slotIdx] => m_bags[bagId][slotIdx];
		private ItemSlot m_selectedSlot;
		private IItem m_selectedItem;

		private void Start()
		{
			for (var i = 0; i < m_bags.Count; i++)
			{
				m_bags[i].Id = i;
			}


			//TODO delete debug stuff
			var debugItem = new MiscItem(new MiscItemTemplate()
			{
				DisplayName = "Debug Misc Item",
				Description = "Debug Misc Item",
				Icon = new SpriteModel(m_debugSprite),
				Rarity = Rarity.Legendary,
				Id = ObjectId.GenerateNewId(),
			})
			{
				MaxQuantity = 10,
				Quantity = 5
			};


			this[0, 0].Add(debugItem);
			this[0, 1].Add(new MiscItem((MiscItemTemplate) debugItem.ItemTemplate));
			this[0, 2].Add(debugItem);
			this[0, 4].Add(debugItem);
			this[0, 3].Add(debugItem);
			this[0, 7].Add(new WeaponItem(new WeaponItemTemplate()
			{
				DisplayName = "Debug Weapon Item",
				Description =
					"Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo ",
				Icon = new SpriteModel(m_debugSprite),
				Rarity = Rarity.Uncommon,
				Id = ObjectId.GenerateNewId(),
				Stats = new StatCollection()
				{
					new Health(150),
					new Strength(35)
				}
			}));
		}

		public void OnBeginDrag(PointerEventData eventData) => CheckDragableItem(eventData);

		private void CheckDragableItem(PointerEventData eventData)
		{
			if (ItemUtilities.TryGetTargetSlot(eventData.pointerEnter, out var targetSlot))
			{
				if (targetSlot.HasItem())
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
			if (ItemUtilities.TryGetTargetSlot(eventData.pointerEnter, out var targetSlot))
			{
				if (targetSlot.IsEmpty(m_selectedItem))
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
					//switch items
					var otherItem = targetSlot.Content;
					targetSlot.Remove();
					m_selectedSlot.Add(otherItem);
					targetSlot.Add(m_selectedItem);
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
			m_dragableIcon.sprite = m_selectedItem.ItemTemplate.Icon.Data;
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
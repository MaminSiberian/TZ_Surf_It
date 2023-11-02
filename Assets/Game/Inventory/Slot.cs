using UnityEngine;
using UnityEngine.EventSystems;

namespace Inventory
{
    public class Slot : UI.Slot, IDropHandler
    {
        private InventoryController inventory;

        protected override void Awake()
        {
            base.Awake();
            inventory = GetComponentInParent<InventoryController>();
        }

        public void OnDrop(PointerEventData eventData)
        {
            GetContainerItem(eventData);
            GetInventoryItem(eventData);
        }
        private void GetContainerItem(PointerEventData eventData)
        {
            var item = eventData.pointerDrag.GetComponent<ItemsContainer.ItemController>();
            if (item == null || !inventory.CanAdd(item, this)) return;

            inventory.AddItemToInventory(item, this);
        }
        private void GetInventoryItem(PointerEventData eventData)
        {
            var item = eventData.pointerDrag.GetComponent<ItemController>();
            if (item == null || inventory.CanAdd(item, this)) return;

            item.transform.SetParent(transform);
            item.transform.localPosition = Vector3.zero;
            OccupySlot();
        }
    }
}

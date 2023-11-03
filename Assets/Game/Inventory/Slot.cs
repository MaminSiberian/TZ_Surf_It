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
            if (item == null) return;

            if (!inventory.CanAdd(item.model.size, this))
            {
                Slot nearestSlot = inventory.FindNearestSuitableSlot(this, item.model.size);
                if (nearestSlot != null)
                    inventory.AddItemToInventory(item, nearestSlot);

                return;
            }
            inventory.AddItemToInventory(item, this);
        }
        private void GetInventoryItem(PointerEventData eventData)
        {
            var item = eventData.pointerDrag.GetComponent<ItemController>();
            if (item == null) return;
            if (!inventory.CanAdd(item.model.size, this))
            {
                Slot nearestSlot = inventory.FindNearestSuitableSlot(this, item.model.size);
                if (nearestSlot != null)
                    inventory.AddItemToInventory(item, nearestSlot);
                else
                    inventory.AddItemToInventory(item, inventory.model.itemsInInventory[item.model]);

                return;
            }

            inventory.AddItemToInventory(item, this);
            OccupySlot();
        }
    }
}

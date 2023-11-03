using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.Progress;

namespace Inventory
{
    public class InventoryController : MonoBehaviour
    {
        [SerializeField] private ItemController itemPrefab;
        [SerializeField] private GameObject grid;
        [SerializeField] private Transform itemParent;

        public InventoryModel model {  get; private set; }
        private List<Slot> slots = new List<Slot>();
        private List<ItemController> items = new List<ItemController>();

        private float localSlotSize;
        private ItemsContainer.ItemsContainer container;

        private void Start()
        {
            container = FindAnyObjectByType<ItemsContainer.ItemsContainer>();
            model = new InventoryModel();
            GetSlots();
            //LoadInventory();
        }
        public void AddItemToInventory(UI.ItemController item, Slot slot)
        {
            var newItem = CreateItem(item.model.itemInfo, item.model.condition, slot.transform);
            items.Add(newItem);
            model.AddItem(newItem.model, slot);
            newItem.transform.SetParent(itemParent);
            RequiredSlots(slot, item.model.size).ForEach(slot => slot.OccupySlot());
            Destroy(item.gameObject);
            items.ForEach(i => Debug.Log(i));
        }
        public void RemoveItemFromInventory(ItemController item)
        {
            Slot slot = model.itemsInInventory[item.model];
            RequiredSlots(slot, item.model.size).ForEach(slot => slot.ReleaseSlot());
            items.Remove(item);
        }
        public void ClearInventory()
        {
            items.ForEach(i => Destroy(i.gameObject));
            slots.ForEach(s => s.ReleaseSlot());
            items.Clear();
            model.ClearInventory();
        }
        public void SaveInventory()
        {
            InventorySaveManager.SaveInventoryData(model.itemsInInventory);
        }
        public void LoadInventory()
        {
            ClearInventory();
            var data = InventorySaveManager.LoadInventoryData();

            for (int i = 0; i < data.Count; i++)
            {
                var key = data.ElementAt(i).Key;
                var value = data.ElementAt(i).Value;
                var item = CreateItem(key.itemInfo, key.condition, value.transform);

                items.Add(item);
                model.AddItem(key, value);
                item.transform.SetParent(itemParent);
                RequiredSlots(value, item.model.size).ForEach(slot => slot.OccupySlot());
            }

        }
        public void ReturnItemToContainer(ItemController item)
        {
            container.AddItem(item.model.itemInfo, item.model.condition);
            RemoveItemFromInventory(item);
            model.RemoveItem(item.model);
            Destroy(item.gameObject);
        }
        private void GetSlots()
        {
            slots = grid.GetComponentsInChildren<Slot>().ToList();
            slots.ForEach(slot => slot.ReleaseSlot());

            localSlotSize = Mathf.Abs(Vector2.Distance(slots[0].transform.localPosition, slots[1].transform.localPosition));
        }
        private ItemController CreateItem(ItemInfo info, float condition, Transform parent)
        {
            var itemObj = Instantiate(itemPrefab, parent);
            itemObj.Initialize(info, condition);
            itemObj.transform.localPosition = Vector3.zero;

            float step = localSlotSize / 2;
            itemObj.transform.localPosition = 
                new Vector2(itemObj.transform.localPosition.x + step * (info.size.x - 1), itemObj.transform.localPosition.y - step * (info.size.y - 1));

            return itemObj;
        }
        public bool CanAdd(UI.ItemController item, Slot slot)
        {
            if (!slot.isVacant) return false;

            List<Slot> reqSlots = RequiredSlots(slot, item.model.size);

            if (reqSlots.Count < item.model.size.x * item.model.size.y || reqSlots.Any(s => !s.isVacant))
            {
                Debug.Log("Not enough slots for this item!");
                return false;
            }

            return true;
        }
        private List<Slot> RequiredSlots(Slot firstSlot, Vector2Int size)
        {
            List<Slot> requiredSlots = new List<Slot>();

            foreach (Slot _slot in slots)
            {
                for (int x = 1; x <= size.x; x++)
                {
                    for (int y = 1; y <= size.y; y++)
                    {
                        if (_slot.transform.localPosition.x == firstSlot.transform.localPosition.x + localSlotSize * (x - 1)
                            && _slot.transform.localPosition.y == firstSlot.transform.localPosition.y - localSlotSize * (y - 1))
                        {
                            requiredSlots.Add(_slot);
                        }
                    }
                }
            }
            requiredSlots = requiredSlots.Distinct().ToList();
            return requiredSlots;
        }
    }
}

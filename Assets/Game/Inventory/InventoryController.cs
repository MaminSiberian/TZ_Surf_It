using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Inventory
{
    public class InventoryController : MonoBehaviour
    {
        [SerializeField] private ItemController itemPrefab;
        [SerializeField] private GameObject grid;
        [SerializeField] private Transform itemParent;

        private InventoryModel model;
        private List<Slot> slots = new List<Slot>();
        private List<ItemController> items = new List<ItemController>();

        private float localSlotSize;

        private void Start()
        {
            model = new InventoryModel();
            GetSlots();
            //LoadInventory();
        }
        public void AddItemToInventory(UI.ItemController item, Slot slot)
        {
            if (!CanAdd(item, slot))
            {
                Debug.Log("Can`t add item!");
                return;
            }
            var newItem = CreateItem(item.model.itemInfo, item.model.condition, slot.transform);
            items.Add(newItem);
            model.AddItem(item.model, item.transform.localPosition);

            newItem.transform.SetParent(itemParent);

            RequiredSlots(slot, item.model.size).ForEach(slot => slot.OccupySlot());
            Destroy(item.gameObject);
        }
        public void RemoveItemFromInventory(UI.ItemController item, Slot slot)
        {
            model.RemoveItem(item.model);
            RequiredSlots(slot, item.model.size).ForEach(slot => slot.ReleaseSlot());
            Destroy(item.gameObject);
        }
        public void ClearInventory()
        {
            slots.ForEach(s => s.ReleaseSlot());
            items.ForEach(i => Destroy(i));
            items.Clear();
            model.ClearInventory();
        }
        public void SaveInventory()
        {
            //save
        }
        public void LoadInventory()
        {
            ClearInventory();
            //load
            UpdateInventory();
        }
        private void UpdateInventory()
        {
            model.ClearInventory();
            items.ForEach(item => model.AddItem(item.model, item.transform.localPosition));
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

            if (reqSlots.Count < item.model.size.x * item.model.size.y) return false;

            return !reqSlots.Any(s => !s.isVacant);
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

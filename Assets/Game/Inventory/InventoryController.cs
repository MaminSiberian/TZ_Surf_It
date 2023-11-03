using System.Collections.Generic;
using System.Linq;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using static UnityEditor.Progress;

namespace Inventory
{
    public class InventoryController : MonoBehaviour
    {
        [SerializeField] private GameObject grid;
        [SerializeField] private Transform itemParent;

        public InventoryModel model {  get; private set; }
        private List<Slot> slots = new List<Slot>();
        private List<ItemController> items = new List<ItemController>();

        private float localSlotSize;
        private ItemsContainer.ItemsContainer container;
        private UI.ItemPool pool;

        private void Start()
        {
            container = FindAnyObjectByType<ItemsContainer.ItemsContainer>();
            pool = FindAnyObjectByType<UI.ItemPool>();
            model = new InventoryModel();
            GetSlots();
            LoadInventory();
        }

        #region inventory
        public void AddItemToInventory(UI.ItemController item, Slot slot)
        {
            var newItem = CreateItem(item.model.itemInfo, item.model.condition, slot.transform);
            item.Deactivate();
            model.RemoveItem(item.model);

            items.Add(newItem);
            model.AddItem(newItem.model, slot);

            newItem.transform.SetParent(itemParent);
            RequiredSlots(slot, newItem.model.size).ForEach(slot => slot.OccupySlot());
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
            items.ForEach(i => i.Deactivate());
            slots.ForEach(s => s.ReleaseSlot());
            items.Clear();
            model.ClearInventory();
        }
        public void ReturnItemToContainer(ItemController item)
        {
            container.AddItem(item.model.itemInfo, item.model.condition);
            RemoveItemFromInventory(item);
            model.RemoveItem(item.model);
            item.Deactivate();
        }
        #endregion

        #region save_load
        public void SaveInventory()
        {
            List<ItemData> data = new List<ItemData>();

            foreach (var item in model.itemsInInventory)
            {
                ItemData itemData = new ItemData()
                {
                    itemName = item.Key.itemName,
                    size = item.Key.size,
                    condition = item.Key.condition,
                    slotPosition = item.Value.transform.localPosition
                };
                data.Add(itemData);
            }

            InventorySaveManager.SaveInventoryData(data);
        }
        public void LoadInventory()
        {
            ClearInventory();
            var data = InventorySaveManager.LoadInventoryData();

            var info = Resources.LoadAll<ItemInfo>("ItemInfo/");

            for (int i = 0; i < data.Count; i++)
            {
                var itemData = data[i];
                Slot slot = slots.FirstOrDefault(s => (Vector2)s.transform.localPosition == itemData.slotPosition);
                ItemInfo itemInfo = info.FirstOrDefault(i => i.itemName == itemData.itemName);
                var item = CreateItem(itemInfo, itemData.condition, slot.transform);

                items.Add(item);
                model.AddItem(item.model, slot);
                item.transform.SetParent(itemParent);
                RequiredSlots(slot, item.model.size).ForEach(slot => slot.OccupySlot());
            }
        }
        #endregion

        private ItemController CreateItem(ItemInfo info, float condition, Transform parent)
        {
            var itemObj = pool.GetInventoryItem(parent);
            itemObj.gameObject.SetActive(true);
            itemObj.Initialize(info, condition);
            itemObj.transform.localPosition = Vector3.zero;

            float step = localSlotSize / 2;
            itemObj.transform.localPosition = 
                new Vector2(itemObj.transform.localPosition.x + step * (info.size.x - 1), itemObj.transform.localPosition.y - step * (info.size.y - 1));

            return itemObj;
        }

        #region slots
        private void GetSlots()
        {
            slots = grid.GetComponentsInChildren<Slot>().ToList();
            slots.ForEach(slot => slot.ReleaseSlot());

            localSlotSize = Mathf.Abs(Vector2.Distance(slots[0].transform.localPosition, slots[1].transform.localPosition));
        }
        public bool CanAdd(Vector2Int size, Slot slot)
        {
            if (!slot.isVacant) return false;

            List<Slot> reqSlots = RequiredSlots(slot, size);

            if (reqSlots.Count < size.x * size.y || reqSlots.Any(s => !s.isVacant))
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
        public Slot FindNearestSuitableSlot(Slot _slot, Vector2Int size)
        {
            List<Slot> neighbourSlots = new List<Slot>();
            Vector3 _slotPos = _slot.transform.localPosition;

            foreach (var slot in slots)
            {
                Vector3 slotPos = slot.transform.localPosition;
                if ((slotPos.x == _slotPos.x
                    || slotPos.x == _slotPos.x + localSlotSize
                    || slotPos.x == _slotPos.x - localSlotSize)
                    && (slotPos.y == _slotPos.y + localSlotSize
                    || slotPos.y == _slotPos.y - localSlotSize))
                {
                    neighbourSlots.Add(slot);
                }
                if ((slotPos.y == _slotPos.y 
                    || slotPos.y == _slotPos.y + localSlotSize
                    || slotPos.y == _slotPos.y - localSlotSize)
                    && (slotPos.x == _slotPos.x + localSlotSize
                    || slotPos.x == _slotPos.x - localSlotSize))
                {
                    neighbourSlots.Add(slot);
                }
            }
            return neighbourSlots.FirstOrDefault(s => CanAdd(size, s));
        }
        #endregion
    }
}

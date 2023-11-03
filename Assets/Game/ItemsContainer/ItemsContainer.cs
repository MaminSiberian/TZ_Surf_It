using UnityEngine;
using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;

namespace ItemsContainer
{
    public class ItemsContainer : MonoBehaviour
    {
        [SerializeField] private int numberOfItems = 4;
        [SerializeField] private UI.ItemPool pool;
        [SerializeField] private GameObject grid;

        private List<Slot> slots = new List<Slot>();

        private void Start()
        {
            GetSlots();
            AddRandomItems();
        }
        public void AddItem(ItemInfo info, float condition)
        {
            if (!slots.Any(s => s.isVacant)) return;

            Slot slot = slots.FirstOrDefault(s => s.isVacant);
            CreateItem(info, condition, slot.transform);
            slot.OccupySlot();
        }
        [Button]
        public void AddRandomItems()
        {
            var items = Resources.LoadAll<ItemInfo>("ItemInfo/");

            for (int i = 0; i < numberOfItems; i++)
            {
                AddItem(items[Random.Range(0, items.Length)], Random.Range(0, 100) / 100f);
            }
        }
        private ItemController CreateItem(ItemInfo info, float condition, Transform parent)
        {
            var itemObj = pool.GetContainerItem(parent);
            itemObj.gameObject.SetActive(true);
            itemObj.Initialize(info, condition);
            return itemObj;
        }
        private void GetSlots()
        {
            slots = grid.GetComponentsInChildren<Slot>().ToList();

            foreach (var slot in slots)
            {
                slot.ReleaseSlot();
            }
        }
    }
}

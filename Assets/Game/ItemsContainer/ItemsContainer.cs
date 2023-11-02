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
        [SerializeField] private ItemController itemPrefab;
        [SerializeField] private GameObject grid;

        private List<Slot> slots = new List<Slot>();
        private float startCondition = 1f;

        private void Start()
        {
            GetSlots();
            CreateRandomItems();
        }
        [Button]
        private void CreateRandomItems()
        {
            var items = Resources.LoadAll<ItemInfo>("ItemInfo/");

            for (int i = 0; i < numberOfItems; i++)
            {
                if (i >= slots.Count) break;
                CreateItem(items[Random.Range(0, items.Length)], startCondition, slots[i].transform);
                slots[i].OccupySlot();
            }
        }
        private ItemController CreateItem(ItemInfo info, float condition, Transform parent)
        {
            var itemObj = Instantiate(itemPrefab, parent);
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

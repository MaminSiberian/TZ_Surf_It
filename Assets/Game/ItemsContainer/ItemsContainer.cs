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

        private void Awake()
        {
            GenerateSlots();
            CreateRandomItems();
        }
        [Button]
        private void CreateRandomItems()
        {
            var items = Resources.LoadAll<ItemInfo>("ItemInfo/");

            for (int i = 0; i < numberOfItems; i++)
            {
                if (i >= slots.Count) break;
                CreateItem(items[Random.Range(0, items.Length)], slots[i].transform);
                slots[i].OccupySlot();
            }
        }
        private void CreateItem(ItemInfo info, Transform parent)
        {
            var itemObj = Instantiate(itemPrefab, parent);
            itemPrefab.Initialize(info, 1);
        }
        private void GenerateSlots()
        {
            slots = grid.GetComponentsInChildren<Slot>().ToList();

            foreach (var slot in slots)
            {
                slot.ReleaseSlot();
            }
        }
    }
}

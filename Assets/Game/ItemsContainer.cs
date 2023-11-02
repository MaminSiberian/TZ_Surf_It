using UnityEngine;
using NaughtyAttributes;
using System.Collections.Generic;

namespace Inventory
{
    public class ItemsContainer : MonoBehaviour
    {
        [SerializeField] private int numberOfItems = 4;
        [SerializeField] private Item itemPrefab;
        [SerializeField] private List<Slot> slots = new List<Slot>();

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
            }
        }
        private void CreateItem(ItemInfo info, Transform parent)
        {
            var itemObj = Instantiate(itemPrefab, parent);
            itemPrefab.Initialize(info, 1);
        }
        private void GenerateSlots()
        {
            foreach (var slot in slots)
            {
                slot.ReleaseSlot();
            }
        }
    }
}

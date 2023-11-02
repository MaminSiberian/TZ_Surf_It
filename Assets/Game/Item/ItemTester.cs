using Inventory;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class ItemTester : MonoBehaviour
{
    [SerializeField] private RectTransform itemParent;
    [SerializeField] private ItemController itemPrefab;

    [Button]
    private void CreateEveryItem()
    {
        var items = Resources.LoadAll<ItemInfo>("ItemInfo/");

        foreach (var item in items)
        {
            var itemObj = Instantiate(itemPrefab);
            itemObj.transform.SetParent(itemParent);
            itemPrefab.Initialize(item, 1);
        }
    }
    [Button]
    private void CreateRandomItems()
    {

    }
}

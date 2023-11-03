using UnityEngine;

namespace UI
{
    public class ItemPool : MonoBehaviour
    {
        [SerializeField] private int poolCount = 10;
        [SerializeField] private ItemsContainer.ItemController containerItem;
        [SerializeField] private Inventory.ItemController inventoryItem;

        private Pool<ItemsContainer.ItemController> containerPool;
        private Pool<Inventory.ItemController> inventoryPool;

        private void Start()
        {
            containerPool = new Pool<ItemsContainer.ItemController>(containerItem, poolCount, transform);
            inventoryPool = new Pool<Inventory.ItemController>(inventoryItem, poolCount, transform);
        }
        public ItemsContainer.ItemController GetContainerItem(Transform parent = null)
        {
            var item = containerPool.GetObject();
            item.transform.SetParent(parent);
            item.transform.localPosition = Vector3.zero;
            item.transform.localScale = Vector3.one;
            return item;
        }
        public Inventory.ItemController GetInventoryItem(Transform parent = null)
        {
            var item = inventoryPool.GetObject();
            item.transform.SetParent(parent);
            item.transform.localPosition = Vector3.zero;
            item.transform.localScale = Vector3.one;
            return item;
        }
    }
}

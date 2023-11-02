using UnityEngine;

namespace Inventory
{
    public class ItemModel
    {
        public string itemName { get; protected set; }
        public Vector2 size { get; protected set; }
        public float condition { get; protected set; }

        public ItemModel(ItemInfo itemInfo, float condition)
        {
            itemName = itemInfo.itemName;

            size = new Vector2(itemInfo.sizeX, itemInfo.sizeY);

            this.condition = Mathf.Clamp01(condition);
        }
    }
}

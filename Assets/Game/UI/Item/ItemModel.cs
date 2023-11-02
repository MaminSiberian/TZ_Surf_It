using UnityEngine;

namespace UI
{
    public class ItemModel
    {
        public string itemName { get; protected set; }
        public Vector2Int size { get; protected set; }
        public float condition { get; protected set; }
        public ItemInfo itemInfo { get; protected set; }

        public ItemModel(ItemInfo itemInfo, float condition)
        {
            this.itemInfo = itemInfo;
            itemName = itemInfo.itemName;

            size = itemInfo.size;

            this.condition = Mathf.Clamp01(condition);
        }
    }
}

using UnityEngine;

namespace Inventory
{
    public class ContainerItemView : ItemView
    {
        public override void Initialize(ItemInfo itemInfo, float condition)
        {
            image.sprite = itemInfo.sprite;
            nameText.text = itemInfo.itemName;
            condText.text = condition.ToString();
        }
    }
}

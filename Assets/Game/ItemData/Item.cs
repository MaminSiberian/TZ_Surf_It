using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
    [RequireComponent(typeof(Image))]
    public class Item : MonoBehaviour
    {
        public string itemName {  get; private set; }
        public Vector2 size { get; private set; }
        public float condition { get; private set; }
        private Image image;

        public void Initialize(ItemInfo itemInfo, float condition)
        {
            image = GetComponent<Image>();
            image.sprite = itemInfo.sprite;

            itemName = itemInfo.itemName;
            size = new Vector2(itemInfo.sizeX, itemInfo.sizeY);

            gameObject.name = itemInfo.name + "Obj";
            this.condition = Mathf.Clamp01(condition);
        }
    }
}

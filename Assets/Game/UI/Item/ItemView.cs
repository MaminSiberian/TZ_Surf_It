using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public abstract class ItemView : MonoBehaviour
    {
        [SerializeField] protected TextMeshProUGUI nameText;
        [SerializeField] protected TextMeshProUGUI condText;
        [SerializeField] protected Image image;

        public abstract void Initialize(ItemInfo itemInfo, float condition);
        public void ScaleImage(Vector2Int size)
        {
            image.transform.localScale = new Vector2(size.x, size.y);           
        }
    }
}

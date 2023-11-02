using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
    public abstract class ItemView : MonoBehaviour
    {
        [SerializeField] protected TextMeshProUGUI nameText;
        [SerializeField] protected TextMeshProUGUI condText;
        [SerializeField] protected Image image;

        public abstract void Initialize(ItemInfo itemInfo, float condition);
    }
}

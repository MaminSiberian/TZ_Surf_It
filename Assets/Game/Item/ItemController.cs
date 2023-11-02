using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Inventory
{
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class ItemController : MonoBehaviour
    {
        [SerializeField] private ItemView _view;

        public ItemModel model { get; protected set; }
        public ItemView view => _view;

        public void Initialize(ItemInfo itemInfo, float condition)
        {
            gameObject.name = itemInfo.itemName + "Obj";
            
            model = new ItemModel(itemInfo, condition);
            view.Initialize(itemInfo, condition);
        }       
    }
}

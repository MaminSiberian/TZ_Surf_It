using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class ItemController : MonoBehaviour
    {
        [SerializeField] private ItemView _view;

        public ItemModel model { get; protected set; }
        public ItemView view => _view;
        private Transform pool;

        protected virtual void Awake()
        {
            pool = FindAnyObjectByType<ItemPool>().transform;
        }

        public virtual void Initialize(ItemInfo itemInfo, float condition)
        {
            gameObject.name = itemInfo.itemName + "Obj";
            
            model = new ItemModel(itemInfo, condition);
            view.Initialize(itemInfo, condition);
        }
        public void Deactivate()
        {
            transform.SetParent(pool);
            gameObject.SetActive(false);
        }
    }
}

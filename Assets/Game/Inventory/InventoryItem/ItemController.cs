using UnityEngine;
using UnityEngine.EventSystems;

namespace Inventory
{
    [RequireComponent(typeof(CanvasGroup))]
    public class ItemController : UI.ItemController, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerClickHandler
    {
        private InventoryController inventory;
        private RectTransform rectTransform;
        private Canvas canvas;
        private CanvasGroup canvasGroup;

        protected override void Awake()
        {
            base.Awake();
            inventory = GetComponentInParent<InventoryController>();
            rectTransform = GetComponent<RectTransform>();
            canvas = GetComponentInParent<Canvas>();
            canvasGroup = GetComponent<CanvasGroup>();
        }
        private void OnEnable()
        {
            canvasGroup.blocksRaycasts = true;
        }
        public override void Initialize(ItemInfo itemInfo, float condition)
        {
            base.Initialize(itemInfo, condition); 
            view.ScaleImage(itemInfo.size);
        }
        public void OnBeginDrag(PointerEventData eventData)
        {
            transform.SetAsLastSibling();
            if (inventory == null) inventory = GetComponentInParent<InventoryController>();
            inventory.RemoveItemFromInventory(this);
            canvasGroup.blocksRaycasts = false;
        }
        public void OnDrag(PointerEventData eventData)
        {
            if (canvas == null) canvas = GetComponentInParent<Canvas>();
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            canvasGroup.blocksRaycasts = true;
            ReturnToSlot();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (inventory == null) inventory = GetComponentInParent<InventoryController>();
            inventory.ReturnItemToContainer(this);
        }
        private void ReturnToSlot()
        {
            if (inventory == null) inventory = GetComponentInParent<InventoryController>();
            inventory.AddItemToInventory(this, inventory.model.itemsInInventory[model]);
        }
    }
}

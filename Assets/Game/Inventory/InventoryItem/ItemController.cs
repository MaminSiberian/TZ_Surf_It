using System.Drawing;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Inventory
{
    [RequireComponent(typeof(CanvasGroup))]
    public class ItemController : UI.ItemController, /*IDragHandler, IBeginDragHandler, IEndDragHandler,*/ IPointerClickHandler
    {
        private InventoryController inventory;
        /*private RectTransform rectTransform;
        private Canvas canvas;
        private CanvasGroup canvasGroup;*/

        protected void Awake()
        {
            inventory = GetComponentInParent<InventoryController>();
            /*rectTransform = GetComponent<RectTransform>();
            canvas = GetComponentInParent<Canvas>();
            canvasGroup = GetComponent<CanvasGroup>();*/
        }
        public override void Initialize(ItemInfo itemInfo, float condition)
        {
            base.Initialize(itemInfo, condition); 
            view.ScaleImage(itemInfo.size);
        }
        /*public void OnBeginDrag(PointerEventData eventData)
        {
            rectTransform.parent.GetComponent<Slot>().ReleaseSlot();
            rectTransform.parent.SetAsLastSibling();
            canvasGroup.blocksRaycasts = false;
        }
        public void OnDrag(PointerEventData eventData)
        {
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            transform.localPosition = Vector3.zero;
            canvasGroup.blocksRaycasts = true;
            rectTransform.parent.GetComponent<Slot>().OccupySlot();
        }*/

        public void OnPointerClick(PointerEventData eventData)
        {
            inventory.ReturnItemToContainer(this);
        }
    }
}

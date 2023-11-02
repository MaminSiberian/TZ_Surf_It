using UnityEngine;
using UnityEngine.EventSystems;

namespace Inventory
{
    public class ContainerItemController : ItemController, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        private RectTransform rectTransform;
        private Canvas canvas;
        private CanvasGroup canvasGroup;

        protected void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            canvas = GetComponentInParent<Canvas>();
            canvasGroup = GetComponent<CanvasGroup>();
        }
        public void OnBeginDrag(PointerEventData eventData)
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
        }
    }
}

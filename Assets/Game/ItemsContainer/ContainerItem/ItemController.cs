using UnityEngine;
using UnityEngine.EventSystems;

namespace ItemsContainer
{
    [RequireComponent(typeof(CanvasGroup))]
    public class ItemController : UI.ItemController, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        private RectTransform rectTransform;
        private Canvas canvas;
        private CanvasGroup canvasGroup;

        protected override void Awake()
        {
            base.Awake();
            rectTransform = GetComponent<RectTransform>();
            canvasGroup = GetComponent<CanvasGroup>();
            canvas = GetComponentInParent<Canvas>();
        }
        private void OnEnable()
        {
            canvasGroup.blocksRaycasts = true;
        }
        public void OnBeginDrag(PointerEventData eventData)
        {
            rectTransform.parent.GetComponent<Slot>().ReleaseSlot();
            rectTransform.parent.SetAsLastSibling();
            canvasGroup.blocksRaycasts = false;
        }
        public void OnDrag(PointerEventData eventData)
        {
            if (canvas == null) canvas = GetComponentInParent<Canvas>();
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            transform.localPosition = Vector3.zero;
            canvasGroup.blocksRaycasts = true;

            var containerSlot = rectTransform.parent.GetComponent<Slot>();
            
            if (containerSlot != null) containerSlot.OccupySlot();
        }
    }
}

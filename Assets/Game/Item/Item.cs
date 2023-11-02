using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

namespace Inventory
{
    [RequireComponent(typeof(Image)), RequireComponent(typeof(CanvasGroup))]
    public class Item : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerClickHandler
    {
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI condText;

        public string itemName {  get; private set; }
        public Vector2 size { get; private set; }
        public float condition { get; private set; }

        private Image image;
        private RectTransform rectTransform;
        private Canvas canvas;
        private CanvasGroup canvasGroup;

        private void Awake()
        {
            image = GetComponent<Image>();
            rectTransform = GetComponent<RectTransform>();
            canvas = GetComponentInParent<Canvas>();
            canvasGroup = GetComponent<CanvasGroup>();
        }
        public void Initialize(ItemInfo itemInfo, float condition)
        {
            image.sprite = itemInfo.sprite;

            itemName = itemInfo.itemName;
            nameText.text = itemName;
            gameObject.name = itemName + "Obj";

            size = new Vector2(itemInfo.sizeX, itemInfo.sizeY);

            this.condition = Mathf.Clamp01(condition);
            condText.text = this.condition.ToString();
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

        public void OnPointerClick(PointerEventData eventData)
        {
            
        }
    }
}

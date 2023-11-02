using UnityEngine;
using UnityEngine.EventSystems;

namespace ItemsContainer
{
    public class Slot : UI.Slot, IDropHandler
    {
        public void OnDrop(PointerEventData eventData)
        {
            var item = eventData.pointerDrag.GetComponent<ItemController>();

            if (item == null || !isVacant) return;

            item.transform.SetParent(transform);
            item.transform.localPosition = Vector3.zero;
            OccupySlot();
        }
    }
}

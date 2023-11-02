using UnityEngine;
using UnityEngine.EventSystems;

namespace Inventory
{
    public class Slot : MonoBehaviour, IDropHandler
    {
        public bool isVacant {  get; private set; }

        public void OccupySlot()
        {
            if (isVacant) isVacant = false;
        }

        public void ReleaseSlot()
        {
            if (!isVacant) isVacant = true; 
        }
        public void OnDrop(PointerEventData eventData)
        {
            var item = eventData.pointerDrag.GetComponent<Item>();

            if (item == null || !isVacant) return;

            item.transform.SetParent(transform);
            item.transform.localPosition = Vector3.zero;
            OccupySlot();
        }
    }
}

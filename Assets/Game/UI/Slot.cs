using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public abstract class Slot : MonoBehaviour, IDropHandler
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
        public abstract void OnDrop(PointerEventData eventData);
    }
}

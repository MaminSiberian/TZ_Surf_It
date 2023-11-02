using UnityEngine;

namespace Inventory
{
    public class Slot : MonoBehaviour
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
    }
}

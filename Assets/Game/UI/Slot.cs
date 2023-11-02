using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public abstract class Slot : MonoBehaviour
    {
        public bool isVacant {  get; private set; }
        private Image image;

        protected virtual void Awake()
        {
            image = GetComponent<Image>();
        }
        public void OccupySlot()
        {
            if (isVacant) isVacant = false;
            image.enabled = false;
        }

        public void ReleaseSlot()
        {
            if (!isVacant) isVacant = true; 
            image.enabled = true;
        }
    }
}

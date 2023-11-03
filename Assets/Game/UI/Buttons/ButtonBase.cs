using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public abstract class ButtonBase : MonoBehaviour
    {
        private Button button;

        private void Awake()
        {
            button = GetComponent<Button>();
        }
        private void OnEnable()
        {
            button.onClick.AddListener(OnButtonClick);
        }
        private void OnDisable()
        {
            button?.onClick.RemoveListener(OnButtonClick);
        }
        protected abstract void OnButtonClick();
    }
}

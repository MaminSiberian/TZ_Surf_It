
namespace UI
{
    public class AddRandomItemsButton : ButtonBase
    {
        private ItemsContainer.ItemsContainer container;

        private void Start()
        {
            container = FindAnyObjectByType<ItemsContainer.ItemsContainer>();
        }
        protected override void OnButtonClick()
        {
            container.AddRandomItems();
        }
    }
}

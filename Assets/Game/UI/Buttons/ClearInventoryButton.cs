
namespace UI
{
    public class ClearInventoryButton : ButtonBase
    {
        private Inventory.InventoryController inventory;

        private void Start()
        {
            inventory = FindAnyObjectByType<Inventory.InventoryController>();
        }
        protected override void OnButtonClick()
        {
            inventory.ClearInventory();
        }
    }
}

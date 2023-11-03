namespace UI
{
    public class LoadInventoryButton : ButtonBase
    {
        private Inventory.InventoryController inventory;

        private void Start()
        {
            inventory = FindAnyObjectByType<Inventory.InventoryController>();
        }
        protected override void OnButtonClick()
        {
            inventory.LoadInventory();
        }
    }
}

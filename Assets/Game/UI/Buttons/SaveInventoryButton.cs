namespace UI
{
    public class SaveInventoryButton : ButtonBase
    {
        private Inventory.InventoryController inventory;

        private void Start()
        {
            inventory = FindAnyObjectByType<Inventory.InventoryController>();
        }
        protected override void OnButtonClick()
        {
            inventory.SaveInventory();
        }
    }
}

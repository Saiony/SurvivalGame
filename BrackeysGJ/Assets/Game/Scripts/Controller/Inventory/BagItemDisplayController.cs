
using BrackeysGJ.Assets.Game.Scripts.Domain.Interface.Items;

namespace BrackeysGJ.Assets.Game.Scripts.Controller.Inventory
{
    public class BagItemDisplayController : BaseItemDisplayController
    {
        private int InventoryPos { get; set; }

        public void Init(int inventoryPos, InventoryItemDisplayListener listener)
        {
            InventoryPos = inventoryPos;
            BaseInit(listener);
        }

        public override void OnItemConsumed(IItem item)
        {
            PlayerItems.Inventory.ConsumeItem(item, InventoryPos);
        }

        public override void OnItemAdded(IItem item)
        {
            PlayerItems.Inventory.AddItem(item, InventoryPos);
        }
    }
}

using An01malia.FirstPerson.InventoryModule.Items;
using UnityEngine;

namespace An01malia.FirstPerson.InventoryModule
{
    public class PlayerInventory : InventoryBaseClass
    {
        public static GameObject Panel;

        #region Properties

        public bool IsOpen
        {
            get => isOpen;
            set
            {
                isOpen = value;

                StoreAndRestore();
            }
        }

        #endregion

        private void Start()
        {
            SetReferences();
            SetStartingItems();
        }

        public void RemoveItem(Item item)
        {
            int amount = item.Amount;

            foreach (Slot slot in slots)
            {
                if (slot.Item != null && slot.Item.IsSame(item))
                {
                    if (amount > slot.Item.Amount)
                    {
                        slot.Item.Amount -= slot.Item.Amount;
                        amount -= slot.Item.Amount;
                    }
                    else
                    {
                        slot.Item.Amount -= amount;
                        return;
                    }
                }
            }
        }

        private void SetReferences()
        {
            slots = Panel.GetComponentsInChildren<Slot>();
        }
    }
}
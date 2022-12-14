using An01malia.FirstPerson.InventoryModule.Items;
using UnityEngine;

namespace An01malia.FirstPerson.InventoryModule
{
    public class PlayerInventory : InventoryBaseClass
    {
        #region Fields

        public static GameObject Panel;

        #endregion

        #region Properties

        public override bool IsOpen
        {
            get => base.IsOpen;
            set
            {
                base.IsOpen = value;

                StoreAndRestore();
            }
        }

        #endregion

        #region Unity Methods

        private void Start()
        {
            SetReferences();
            SetStartingItems();
        }

        #endregion

        #region Public Methods

        public void RemoveItem(Item item)
        {
            int amount = item.Amount;

            foreach (Slot slot in Slots)
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

        #endregion

        #region Private Methods

        private void SetReferences()
        {
            Slots = Panel.GetComponentsInChildren<Slot>();
        }

        #endregion
    }
}
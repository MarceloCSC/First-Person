using UnityEngine;

namespace FirstPerson.Inventory
{

    public class Container : InventoryBaseClass
    {

        public static GameObject Panel;

        private bool neverBeenOpened = true;


        #region Properties
        public bool IsOpen
        {
            get => isOpen;
            set
            {
                isOpen = value;

                if (neverBeenOpened)
                {
                    SetStartingItems();
                    neverBeenOpened = false;
                }

                StoreAndRestore();
            }
        }
        #endregion

        #region Cached references
        private PlayerInventory inventory;
        #endregion


        private void Start()
        {
            SetReferences();
        }

        #region On Click Event
        public void TransferAll()
        {
            foreach (Slot slot in slots)
            {
                if (slot.Item != null)
                {
                    inventory.AddItems(slot.Item);
                }
            }
        }
        #endregion

        private void SetReferences()
        {
            inventory = References.Player.GetComponent<PlayerInventory>();
            slots = Panel.GetComponentsInChildren<Slot>();
        }

    }

}

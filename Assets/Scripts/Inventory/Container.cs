using An01malia.FirstPerson.PlayerModule;
using UnityEngine;

namespace An01malia.FirstPerson.InventoryModule
{
    public class Container : InventoryBaseClass
    {
        #region Fields

        public static GameObject Panel;

        private bool _neverBeenOpened = true;
        private PlayerInventory _inventory;

        #endregion

        #region Properties

        public override bool IsOpen
        {
            get => base.IsOpen;
            set
            {
                base.IsOpen = value;

                if (_neverBeenOpened)
                {
                    SetStartingItems();

                    _neverBeenOpened = false;
                }

                StoreAndRestore();
            }
        }

        #endregion

        #region Unity Methods

        private void Start()
        {
            SetReferences();
        }

        #endregion

        #region Public Methods

        public void TransferAll()
        {
            foreach (Slot slot in Slots)
            {
                if (slot.Item != null)
                {
                    _inventory.AddItems(slot.Item);
                }
            }
        }

        #endregion

        #region Private Methods

        private void SetReferences()
        {
            Slots = Panel.GetComponentsInChildren<Slot>();
            _inventory = Player.Transform.GetComponent<PlayerInventory>();
        }

        #endregion
    }
}
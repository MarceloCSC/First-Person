using An01malia.FirstPerson.InventoryModule;
using An01malia.FirstPerson.ItemModule;
using UnityEngine;

namespace An01malia.FirstPerson.UserInterfaceModule.Inventory
{
    public class UIInventoryButtons : MonoBehaviour
    {
        #region Fields

        private Container _container;

        #endregion

        #region Public Methods

        public void ExamineItem()
        {
            if (Slot.ItemSelected != null)
            {
                // trigger inspection state
            }
        }

        public void StopExamine()
        {
            // trigger idle state
        }

        public void UseItem()
        {
            if (Slot.ItemSelected != null)
            {
                print("using " + Slot.ItemSelected.Root.Name);
                Slot.ItemSelected.Amount--;
            }
        }

        public void EquipItem()
        {
            if (Slot.ItemSelected != null)
            {
                print("equipping " + Slot.ItemSelected.Root.Name);
            }
        }

        public void DropItem()
        {
            if (Slot.ItemSelected != null)
            {
                ItemPooler.Instance.ItemDatabase.InstantiateItem(Slot.ItemSelected.Root.Id);
                Slot.ItemSelected.Amount--;
            }
        }

        public void HandleTransfer()
        {
            if (_container == null)
            {
                _container = FindObjectOfType<Container>();
            }

            _container.TransferAll();
        }

        #endregion
    }
}
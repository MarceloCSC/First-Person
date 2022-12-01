using An01malia.FirstPerson.Inspection;
using An01malia.FirstPerson.InventoryModule;
using UnityEngine;

namespace An01malia.FirstPerson.UIModule
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
                if (ItemPooler.Instance.ItemsToExamine.TryGetValue(Slot.ItemSelected.Root.ID, out GameObject item))
                {
                    PlayerInspection.ItemToExamine = item;
                    PlayerInspection.StartExamine(true);
                }
            }
        }

        public void StopExamine()
        {
            PlayerInspection.StartExamine(false);
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
                ItemPooler.Instance.ItemDatabase.InstantiateItem(Slot.ItemSelected.Root.ID);
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
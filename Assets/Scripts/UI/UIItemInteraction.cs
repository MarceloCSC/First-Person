using An01malia.FirstPerson.Inspection;
using An01malia.FirstPerson.InventoryModule;
using UnityEngine;

namespace An01malia.FirstPerson.UIModule
{
    public class UIItemInteraction : MonoBehaviour
    {
        #region Fields

        [SerializeField] private ItemDatabase _itemDatabase;

        #endregion

        #region Public Methods

        public void ExamineItem()
        {
            if (Slot.ItemSelected != null)
            {
                if (ItemPooler.Instance.itemsToExamine.TryGetValue(Slot.ItemSelected.Root.ID, out GameObject item))
                {
                    PlayerItemInspection.ItemToExamine = item;
                    PlayerItemInspection.StartExamine(true);
                }
            }
        }

        public void StopExamine()
        {
            PlayerItemInspection.StartExamine(false);
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
                _itemDatabase.InstantiateItem(Slot.ItemSelected.Root.ID);
                Slot.ItemSelected.Amount--;
            }
        }

        #endregion
    }
}
using UnityEngine;
using FirstPerson.Inventory;
using FirstPerson.Examine;

namespace FirstPerson.UI
{

    public class UIItemInteraction : MonoBehaviour
    {

        [SerializeField] ItemDatabase itemDatabase = null;


        public void ExamineItem()
        {
            if (Slot.ItemSelected != null)
            {
                if (ItemPooler.Instance.itemsToExamine.TryGetValue(Slot.ItemSelected.Root.ID, out GameObject item))
                {
                    PlayerExamine.ItemToExamine = item;
                    PlayerExamine.StartExamine(true);
                }
            }
        }

        public void StopExamine()
        {
            PlayerExamine.StartExamine(false);
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
                itemDatabase.InstantiateItem(Slot.ItemSelected.Root.ID);
                Slot.ItemSelected.Amount--;
            }
        }

    }

}

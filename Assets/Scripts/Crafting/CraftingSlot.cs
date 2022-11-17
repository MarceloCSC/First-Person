using UnityEngine;
using An01malia.FirstPerson.Inventory;

namespace An01malia.FirstPerson.Crafting
{

    public class CraftingSlot : SlotBaseClass
    {

        private bool hasEnough;

        private Color unavailable = Color.grey;


        #region Properties
        public Item Item
        {
            get => item;
            set
            {
                item = value;

                image.sprite = item?.Root.Icon;
                image.color = item != null ? defaultColor : invisible;

                amountText.enabled = item != null && item.Amount > 1;

                if (amountText.enabled)
                {
                    amountText.text = item.Amount.ToString();
                }
            }
        }

        public bool HasEnough
        {
            get => hasEnough;
            set
            {
                hasEnough = value;

                image.color = hasEnough == true ? defaultColor : unavailable;
            }
        }
        #endregion

    }

}

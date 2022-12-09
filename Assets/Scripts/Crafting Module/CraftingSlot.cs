using An01malia.FirstPerson.InventoryModule;
using An01malia.FirstPerson.InventoryModule.Items;
using UnityEngine;

namespace An01malia.FirstPerson.CraftingModule
{
    public class CraftingSlot : SlotBaseClass
    {
        #region Fields

        private bool _hasEnough;

        private Color _unavailable = Color.grey;

        #endregion

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
            get => _hasEnough;
            set
            {
                _hasEnough = value;

                image.color = _hasEnough == true ? defaultColor : _unavailable;
            }
        }

        #endregion
    }
}
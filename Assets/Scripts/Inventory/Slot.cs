using UnityEngine;
using UnityEngine.EventSystems;

namespace An01malia.FirstPerson.Inventory
{

    public class Slot : SlotBaseClass
    {

        private static Item itemSelected;

        private bool isSelected;
        private Color selected = Color.grey;


        #region Properties
        public Item Item
        {
            get => item;
            set
            {
                if (item == value) { return; }

                OnValueChanged(value);

                image.sprite = item?.Root.Icon;
                image.color = item != null ? defaultColor : invisible;

                AmountDisplay(item);
            }
        }

        public static Item ItemSelected
        {
            get => itemSelected;
            set
            {
                if (value == itemSelected) { return; }

                itemSelected = value;

                if (value == null && !EventSystem.current.alreadySelecting)
                {
                    EventSystem.current.SetSelectedGameObject(null);
                }
            }
        }

        protected bool IsSelected
        {
            get => isSelected;
            set
            {
                isSelected = value;
                image.color = isSelected ? selected : defaultColor;
            }
        }
        #endregion


        public void HideContents(bool isHidden)
        {
            image.enabled = !isHidden;

            if (item != null && item.Amount > 1)
            {
                amountText.enabled = !isHidden;
            }
        }

        private void AmountDisplay(Item item)
        {
            amountText.enabled = item != null && item.Amount > 1;

            if (amountText.enabled)
            {
                amountText.text = item.Amount.ToString();
            }
            else if (item != null && item.Amount <= 0)
            {
                Item = null;
                ItemSelected = null;
            }
        }

        private void OnValueChanged(Item value)
        {
            if (item != null)
            {
                item.OnAmountChanged -= AmountDisplay;
            }

            item = value;

            if (item != null)
            {
                item.OnAmountChanged += AmountDisplay;
            }
        }

    }

}

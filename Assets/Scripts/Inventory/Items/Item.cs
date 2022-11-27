using System;
using UnityEngine;

namespace An01malia.FirstPerson.InventoryModule
{

    [Serializable]
    public class Item
    {

        public event Action<Item> OnAmountChanged = delegate { };


        [SerializeField] ItemObject item = null;
        [SerializeField] int currentAmount = 1;


        #region Properties
        public ItemObject Root => item;

        public int Amount
        {
            get => currentAmount;
            set
            {
                currentAmount = value;
                OnAmountChanged?.Invoke(this);
            }
        }

        public int AmountToBeFilled => item.MaxAmount - currentAmount;

        #endregion


        #region Constructor
        public Item(ItemObject itemObject, int amount)
        {
            item = itemObject;
            currentAmount = amount;
        }
        #endregion


        public bool CanStack(Item item, int amount = 1)
        {
            return IsSame(item)
                && currentAmount + amount <= this.item.MaxAmount;
        }

        public bool CanStack(Item item, out int amountToFill)
        {
            amountToFill = AmountToBeFilled;

            return IsSame(item)
                && currentAmount != this.item.MaxAmount;
        }

        public bool IsSame(Item item)
        {
            return this.item.ID == item.Root.ID;
        }

        public bool IsSame(string itemID)
        {
            return item.ID == itemID;
        }

    }

}

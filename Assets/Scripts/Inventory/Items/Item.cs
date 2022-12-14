using System;
using UnityEngine;

namespace An01malia.FirstPerson.InventoryModule.Items
{
    [Serializable]
    public class Item
    {
        #region Delegates

        public event Action<Item> OnAmountChanged = delegate { };

        #endregion

        #region Fields

        [SerializeField] private ItemObject _item;
        [SerializeField] private int _currentAmount = 1;

        #endregion

        #region Properties

        public ItemObject Root => _item;

        public int Amount
        {
            get => _currentAmount;
            set
            {
                _currentAmount = value;
                OnAmountChanged?.Invoke(this);
            }
        }

        public int AmountToBeFilled => _item.MaxAmount - _currentAmount;

        #endregion

        #region Constructor

        public Item(ItemObject itemObject, int amount)
        {
            _item = itemObject;
            _currentAmount = amount;
        }

        #endregion

        #region Public Methods

        public bool CanStack(Item item, out int amountToFill)
        {
            amountToFill = AmountToBeFilled;

            return IsSame(item)
                && _currentAmount != _item.MaxAmount;
        }

        public bool CanStack(Item item, int amount = 1) => IsSame(item) && _currentAmount + amount <= _item.MaxAmount;

        public bool IsSame(Item item) => _item.ID == item.Root.ID;

        public bool IsSame(string itemID) => _item.ID == itemID;

        #endregion
    }
}
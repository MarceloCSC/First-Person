using System;
using UnityEngine;

namespace An01malia.FirstPerson.ItemModule.Items
{
    [Serializable]
    public class InventoryItem : IItem
    {
        #region Delegates

        public event Action<InventoryItem> OnAmountChanged = delegate { };

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

        public InventoryItem(ItemObject itemObject, int amount)
        {
            _item = itemObject;
            _currentAmount = amount;
        }

        #endregion

        #region Public Methods

        public bool CanStack(InventoryItem item, out int amountToFill)
        {
            amountToFill = AmountToBeFilled;

            return IsSame(item)
                && _currentAmount != _item.MaxAmount;
        }

        public bool CanStack(InventoryItem item, int amount = 1) => IsSame(item) && _currentAmount + amount <= _item.MaxAmount;

        public bool IsSame(InventoryItem item) => _item.Id == item.Root.Id;

        public bool IsSame(string itemId) => _item.Id == itemId;

        #endregion
    }
}
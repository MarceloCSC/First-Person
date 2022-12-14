using An01malia.FirstPerson.InventoryModule.Items;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace An01malia.FirstPerson.InventoryModule
{
    public abstract class InventoryBaseClass : MonoBehaviour, IContainer
    {
        #region Fields

        [SerializeField] protected ItemDatabase Database;
        [SerializeField] protected Item[] StartingItems;

        protected Slot[] Slots;
        protected Dictionary<Slot, Tuple<string, int>> StoredItems;

        #endregion

        #region Properties

        public virtual bool IsOpen { get; set; }

        #endregion

        #region Public Methods

        public virtual void SetStartingItems()
        {
            ClearAllItems();

            for (int i = 0; i < StartingItems.Length; i++)
            {
                AddItems(StartingItems[i]);
            }

            StoreInDictionary();
        }

        public virtual void ClearAllItems()
        {
            for (int i = 0; i < Slots.Length; i++)
            {
                Slots[i].Item = null;
            }
        }

        public virtual void AddItems(Item item)
        {
            AddToStack(item);

            if (item.Amount <= 0) { return; }

            DivideIntoStacks(item);

            if (item.Amount <= 0) { return; }

            AddRemaining(item);
        }

        public virtual void AddToStack(Item item)
        {
            for (int i = 0; i < Slots.Length; i++)
            {
                if (Slots[i].Item != null && Slots[i].Item.CanStack(item, out int amountToFill))
                {
                    if (item.Amount <= amountToFill)
                    {
                        Slots[i].Item.Amount += item.Amount;
                        item.Amount = 0;
                        return;
                    }
                    else
                    {
                        Slots[i].Item.Amount += amountToFill;
                        item.Amount -= amountToFill;
                    }
                }
            }
        }

        public virtual void DivideIntoStacks(Item item)
        {
            int maxAmount = item.Root.MaxAmount;

            if (item.Amount <= maxAmount) { return; }

            for (; item.Amount > maxAmount; item.Amount -= maxAmount)
            {
                for (int i = 0; i < Slots.Length; i++)
                {
                    if (Slots[i].Item == null)
                    {
                        Slots[i].Item = Database.RetrieveItem(item.Root.ID, maxAmount);
                        break;
                    }
                }
            }
        }

        public virtual void AddRemaining(Item item)
        {
            for (int i = 0; i < Slots.Length; i++)
            {
                if (Slots[i].Item == null)
                {
                    Slots[i].Item = Database.RetrieveItem(item.Root.ID, item.Amount);
                    item.Amount -= item.Amount;
                    return;
                }
            }
        }

        public virtual int CountItems(Item item)
        {
            int totalAmount = 0;

            foreach (Slot slot in Slots)
            {
                if (slot.Item != null && slot.Item.IsSame(item))
                {
                    totalAmount += slot.Item.Amount;
                }
            }

            return totalAmount;
        }

        #endregion

        #region Protected Methods

        protected void StoreAndRestore()
        {
            if (IsOpen)
            {
                RestoreItems();
            }
            else
            {
                StoreInDictionary();
            }
        }

        protected void StoreInDictionary()
        {
            StoredItems = new Dictionary<Slot, Tuple<string, int>>();

            foreach (Slot slot in Slots)
            {
                if (slot.Item != null)
                {
                    var item = Tuple.Create(slot.Item.Root.ID, slot.Item.Amount);

                    StoredItems.Add(slot, item);
                }
            }
        }

        protected void RestoreItems()
        {
            ClearAllItems();

            for (int i = 0; i < Slots.Length; i++)
            {
                foreach (Slot key in StoredItems.Keys)
                {
                    if (key.Equals(Slots[i]))
                    {
                        Item item = Database.RetrieveItem(StoredItems[key].Item1, StoredItems[key].Item2);

                        Slots[i].Item = item;
                    }
                }
            }
        }

        #endregion
    }
}
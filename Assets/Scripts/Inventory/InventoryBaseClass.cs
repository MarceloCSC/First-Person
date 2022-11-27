using System;
using System.Collections.Generic;
using UnityEngine;

namespace An01malia.FirstPerson.InventoryModule
{

    public abstract class InventoryBaseClass : MonoBehaviour, IContainer
    {

        [SerializeField] protected ItemDatabase database;
        [SerializeField] protected Item[] startingItems = null;

        protected Slot[] slots;
        protected Dictionary<Slot, Tuple<string, int>> storedItems;

        protected bool isOpen;


        protected void StoreAndRestore()
        {
            if (isOpen)
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
            storedItems = new Dictionary<Slot, Tuple<string, int>>();

            foreach (Slot slot in slots)
            {
                if (slot.Item != null)
                {
                    var item = Tuple.Create(slot.Item.Root.ID, slot.Item.Amount);

                    storedItems.Add(slot, item);
                }
            }
        }

        protected void RestoreItems()
        {
            ClearAllItems();

            for (int i = 0; i < slots.Length; i++)
            {
                foreach (Slot key in storedItems.Keys)
                {
                    if (key.Equals(slots[i]))
                    {
                        Item item = database.RetrieveItem(storedItems[key].Item1, storedItems[key].Item2);

                        slots[i].Item = item;
                    }
                }
            }
        }

        public virtual void SetStartingItems()
        {
            ClearAllItems();

            for (int i = 0; i < startingItems.Length; i++)
            {
                AddItems(startingItems[i]);
            }

            StoreInDictionary();
        }

        public virtual void ClearAllItems()
        {
            for (int i = 0; i < slots.Length; i++)
            {
                slots[i].Item = null;
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
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].Item != null && slots[i].Item.CanStack(item, out int amountToFill))
                {
                    if (item.Amount <= amountToFill)
                    {
                        slots[i].Item.Amount += item.Amount;
                        item.Amount = 0;
                        return;
                    }
                    else
                    {
                        slots[i].Item.Amount += amountToFill;
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
                for (int i = 0; i < slots.Length; i++)
                {
                    if (slots[i].Item == null)
                    {
                        slots[i].Item = database.RetrieveItem(item.Root.ID, maxAmount);
                        break;
                    }
                }
            }
        }

        public virtual void AddRemaining(Item item)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].Item == null)
                {
                    slots[i].Item = database.RetrieveItem(item.Root.ID, item.Amount);
                    item.Amount -= item.Amount;
                    return;
                }
            }
        }

        public virtual int CountItems(Item item)
        {
            int totalAmount = 0;

            foreach (Slot slot in slots)
            {
                if (slot.Item != null && slot.Item.IsSame(item))
                {
                    totalAmount += slot.Item.Amount;
                }
            }

            return totalAmount;
        }

    }

}

﻿using An01malia.FirstPerson.Core.References;
using An01malia.FirstPerson.ItemModule.Items;
using UnityEngine;

namespace An01malia.FirstPerson.ItemModule
{
    [CreateAssetMenu(fileName = "NewItemDatabase", menuName = "Inventory/New Item Database")]
    public class ItemDatabase : ScriptableObject
    {
        #region Fields

        [SerializeField] private ItemObject[] _allItems;

        #endregion

        #region Properties

        public ItemObject[] AllItems => _allItems;

        #endregion

        #region Public Methods

        public InventoryItem RetrieveItem(string itemID, int amount = 1)
        {
            foreach (ItemObject item in _allItems)
            {
                if (item.ID == itemID)
                {
                    return new InventoryItem(item, amount);
                }
            }

            return null;
        }

        public void InstantiateItem(string itemID)
        {
            foreach (ItemObject item in _allItems)
            {
                if (item.ID == itemID)
                {
                    Instantiate(item.Prefab, Player.Transform.position, Quaternion.identity);
                }
            }
        }

        #endregion
    }
}
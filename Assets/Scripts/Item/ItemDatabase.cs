using An01malia.FirstPerson.ItemModule.Items;
using An01malia.FirstPerson.PlayerModule;
using UnityEngine;

namespace An01malia.FirstPerson.ItemModule
{
    [CreateAssetMenu(fileName = "NewItemDatabase", menuName = "Items/New Item Database")]
    public class ItemDatabase : ScriptableObject
    {
        #region Fields

        [SerializeField] private ItemObject[] _allItems;

        #endregion

        #region Properties

        public ItemObject[] AllItems => _allItems;

        #endregion

        #region Public Methods

        public InventoryItem RetrieveItem(string itemId, int amount = 1)
        {
            foreach (ItemObject item in _allItems)
            {
                if (item.Id == itemId)
                {
                    return new InventoryItem(item, amount);
                }
            }

            return null;
        }

        public void InstantiateItem(string itemId)
        {
            foreach (ItemObject item in _allItems)
            {
                if (item.Id == itemId)
                {
                    Instantiate(item.Prefab, Player.Transform.position, Quaternion.identity);
                }
            }
        }

        #endregion
    }
}
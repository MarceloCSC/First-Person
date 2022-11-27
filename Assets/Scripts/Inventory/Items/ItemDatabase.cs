using An01malia.FirstPerson.Core.References;
using UnityEngine;

namespace An01malia.FirstPerson.InventoryModule
{
    [CreateAssetMenu(fileName = "NewItemDatabase", menuName = "Inventory/New Item Database")]
    public class ItemDatabase : ScriptableObject
    {
        public ItemObject[] allItems;

        public Item RetrieveItem(string itemID, int amount = 1)
        {
            foreach (ItemObject item in allItems)
            {
                if (item.ID == itemID)
                {
                    return new Item(item, amount);
                }
            }
            return null;
        }

        public void InstantiateItem(string itemID)
        {
            foreach (ItemObject item in allItems)
            {
                if (item.ID == itemID)
                {
                    Instantiate(item.Prefab, Player.Transform.position, Quaternion.identity);
                }
            }
        }
    }
}
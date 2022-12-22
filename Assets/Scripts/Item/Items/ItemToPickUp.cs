using An01malia.FirstPerson.Core.References;
using An01malia.FirstPerson.InventoryModule;
using UnityEngine;

namespace An01malia.FirstPerson.ItemModule.Items
{
    public class ItemToPickUp : MonoBehaviour, IItem
    {
        #region Fields

        [SerializeField] private ItemObject _item;
        [SerializeField] private int _quantity = 1;

        #endregion

        #region Properties

        public ItemObject Root => _item;

        #endregion

        #region Public Methods

        public void AddToInventory()
        {
            Player.Transform.GetComponent<PlayerInventory>().AddItems(new InventoryItem(_item, _quantity));

            gameObject.SetActive(false);
        }

        #endregion
    }
}
using An01malia.FirstPerson.Core.References;
using An01malia.FirstPerson.InventoryModule;
using An01malia.FirstPerson.InventoryModule.Items;
using UnityEngine;

namespace An01malia.FirstPerson.InteractionModule.Interactive
{
    public class ItemToPickUp : MonoBehaviour, IInteractive
    {
        #region Fields

        [SerializeField] private Item _item;

        #endregion

        #region Public Methods

        public void StartInteraction()
        {
            Player.Transform.GetComponent<PlayerInventory>().AddItems(_item);

            gameObject.SetActive(false);
        }

        #endregion
    }
}
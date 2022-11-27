using An01malia.FirstPerson.Core.References;
using An01malia.FirstPerson.InventoryModule;
using UnityEngine;

namespace An01malia.FirstPerson.InteractionModule
{
    public class Pickable : MonoBehaviour, IInteractive
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
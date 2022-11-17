using An01malia.FirstPerson.Inventory;
using UnityEngine;

namespace An01malia.FirstPerson.Interaction
{
    public class Pickupable : MonoBehaviour, IInteractive
    {
        #region Fields

        [SerializeField] private Item _item = null;

        private PlayerInventory _inventory;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            SetReferences();
        }

        #endregion

        #region Public Methods

        public void StartInteraction()
        {
            _inventory.AddItems(_item);

            gameObject.SetActive(false);
        }

        #endregion

        #region Private Methods

        private void SetReferences()
        {
            _inventory = References.Player.GetComponent<PlayerInventory>();
        }

        #endregion
    }
}
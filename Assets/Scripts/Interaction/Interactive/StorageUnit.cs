using An01malia.FirstPerson.InventoryModule;
using UnityEngine;

namespace An01malia.FirstPerson.InteractionModule.Interactive
{
    [RequireComponent(typeof(Container))]
    public class StorageUnit : MonoBehaviour, IInteractive
    {
        #region Fields

        private Container _container;

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
            _container.IsOpen = !_container.IsOpen;
        }

        #endregion

        #region Private Methods

        private void SetReferences()
        {
            _container = GetComponent<Container>();
        }

        #endregion
    }
}
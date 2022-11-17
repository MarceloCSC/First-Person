using An01malia.FirstPerson.Inventory;
using An01malia.FirstPerson.UI;
using UnityEngine;

namespace An01malia.FirstPerson.Interaction
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

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject == References.Player && Container.Panel.activeSelf)
            {
                _container.IsOpen = false;
                UIPanels.ToggleUIPanel(Container.Panel);
            }
        }

        #endregion

        #region Public Methods

        public void StartInteraction()
        {
            _container.IsOpen = !_container.IsOpen;
            UIPanels.ToggleUIPanel(Container.Panel);
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
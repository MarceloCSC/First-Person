using An01malia.FirstPerson.Crafting;
using An01malia.FirstPerson.InventoryModule;
using UnityEngine;

namespace An01malia.FirstPerson.UIModule
{
    public class UIPanelButtons : MonoBehaviour
    {
        #region Fields

        private Container _container;
        private CraftItems _crafting;

        #endregion

        #region Public Methods

        public void HandleTransfer()
        {
            if (_container == null)
            {
                _container = FindObjectOfType<Container>();
            }

            _container.TransferAll();
        }

        public void HandleCrafting()
        {
            if (_crafting == null)
            {
                _crafting = FindObjectOfType<CraftItems>();
            }

            //
        }

        #endregion
    }
}
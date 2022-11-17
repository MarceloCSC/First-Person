using UnityEngine;
using An01malia.FirstPerson.Inventory;
using An01malia.FirstPerson.Crafting;

namespace An01malia.FirstPerson.UI
{

    public class UIPanelButtons : MonoBehaviour
    {

        #region Cached references
        private Container container;
        private CraftItems crafting;
        #endregion


        public void HandleTransfer()
        {
            if (container == null)
            {
                container = FindObjectOfType<Container>();
            }

            container.TransferAll();
        }

        public void HandleCrafting()
        {
            if (crafting == null)
            {
                crafting = FindObjectOfType<CraftItems>();
            }

            //
        }

    }

}

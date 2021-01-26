using UnityEngine;
using FirstPerson.Inventory;
using FirstPerson.Crafting;

namespace FirstPerson.UI
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

using UnityEngine;
using FirstPerson.UI;
using FirstPerson.Inventory;

namespace FirstPerson.Interaction
{

    [RequireComponent(typeof(Container))]
    public class StorageUnit : Interactable
    {

        #region Cached references
        private Container container;
        #endregion


        protected override void InteractWith()
        {
            if (IsSame())
            {
                container.IsOpen = !container.IsOpen;
                UIPanels.ToggleUIPanel(Container.Panel);
            }
        }

        protected override void OutOfRange()
        {
            if (Container.Panel.activeSelf)
            {
                container.IsOpen = false;
                UIPanels.ToggleUIPanel(Container.Panel);
            }
        }

        protected override void SetReferences()
        {
            base.SetReferences();
            container = GetComponent<Container>();
        }

    }

}

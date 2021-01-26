using UnityEngine;
using FirstPerson.UI;
using FirstPerson.Crafting;

namespace FirstPerson.Interaction
{

    [RequireComponent(typeof(CraftItems))]
    public class CraftingTable : Interactable
    {

        #region Cached references
        private CraftItems crafting;
        #endregion


        protected override void InteractWith()
        {
            if (IsSame())
            {
                UIPanels.ToggleUIPanel(CraftItems.Panel);
                crafting.CheckRecipes();
            }
        }

        protected override void OutOfRange()
        {
            if (CraftItems.Panel.activeSelf)
            {
                UIPanels.ToggleUIPanel(CraftItems.Panel);
            }
        }

        protected override void SetReferences()
        {
            base.SetReferences();
            crafting = GetComponent<CraftItems>();
        }

    }

}

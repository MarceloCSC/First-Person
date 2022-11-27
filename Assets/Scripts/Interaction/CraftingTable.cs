using An01malia.FirstPerson.Core.References;
using An01malia.FirstPerson.Crafting;
using An01malia.FirstPerson.UIModule;
using UnityEngine;

namespace An01malia.FirstPerson.InteractionModule
{
    [RequireComponent(typeof(CraftItems))]
    public class CraftingTable : MonoBehaviour, IInteractive
    {
        #region Fields

        private CraftItems _crafting;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            SetReferences();
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.transform == Player.Transform && CraftItems.Panel.activeSelf)
            {
                UIPanels.ToggleUIPanel(CraftItems.Panel);
            }
        }

        #endregion

        #region Public Methods

        public void StartInteraction()
        {
            UIPanels.ToggleUIPanel(CraftItems.Panel);
            _crafting.CheckRecipes();
        }

        #endregion

        #region Private Methods

        private void SetReferences()
        {
            _crafting = GetComponent<CraftItems>();
        }

        #endregion
    }
}
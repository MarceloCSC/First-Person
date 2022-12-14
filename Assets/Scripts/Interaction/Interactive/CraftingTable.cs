using An01malia.FirstPerson.CraftingModule;
using An01malia.FirstPerson.UIModule;
using UnityEngine;

namespace An01malia.FirstPerson.InteractionModule.Interactive
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
            if (other.CompareTag("Player") && CraftItems.Panel.activeSelf)
            {
                UIPanelManager.ToggleUIPanel(CraftItems.Panel);
            }
        }

        #endregion

        #region Public Methods

        public void StartInteraction()
        {
            UIPanelManager.ToggleUIPanel(CraftItems.Panel);

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
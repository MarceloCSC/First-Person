using An01malia.FirstPerson.Inspection;
using An01malia.FirstPerson.UIModule;
using UnityEngine;

namespace An01malia.FirstPerson.InteractionModule
{
    public class Inspectable : MonoBehaviour, IInteractive
    {
        #region Public Methods

        public void StartInteraction()
        {
            UIPanelManager.ToggleUIPanel(PlayerInspection.Panel);
        }

        #endregion
    }
}
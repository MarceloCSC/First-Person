using An01malia.FirstPerson.Core.References;
using An01malia.FirstPerson.UIModule;
using UnityEngine;

namespace An01malia.FirstPerson.Inspection
{
    public class PlayerInspection : MonoBehaviour
    {
        #region Fields

        public static GameObject Panel;
        public static GameObject ItemToExamine;

        #endregion

        #region Unity Methods

        private void Start()
        {
            UI.LightSource.SetActive(false);
        }

        #endregion

        #region Public Methods

        public static void StartExamine(bool isActive)
        {
            UIPanelManager.ToggleInspectionUI(isActive);
            ItemToExamine.SetActive(isActive);
            UI.LightSource.SetActive(isActive);
        }

        #endregion
    }
}
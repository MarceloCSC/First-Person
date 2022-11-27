using An01malia.FirstPerson.UIModule;
using UnityEngine;

namespace An01malia.FirstPerson.Inspection
{
    public class PlayerItemInspection : MonoBehaviour
    {
        #region Fields

        public static GameObject Panel;
        public static GameObject ItemToExamine;
        public static GameObject LightSource;

        #endregion

        #region Unity Methods

        private void Start()
        {
            LightSource.SetActive(false);
        }

        #endregion

        #region Public Methods

        public static void StartExamine(bool isActive)
        {
            UIPanels.ToggleInspectionUI(isActive);
            ItemToExamine.SetActive(isActive);
            LightSource.SetActive(isActive);
        }

        #endregion
    }
}
using UnityEngine;
using An01malia.FirstPerson.UI;

namespace An01malia.FirstPerson.Inspection
{

    public class PlayerItemInspection : MonoBehaviour
    {

        public static GameObject Panel;
        public static GameObject ItemToExamine;
        public static GameObject LightSource;


        private void Start()
        {
            LightSource.SetActive(false);
        }

        public static void StartExamine(bool isActive)
        {
            UIPanels.ToggleExamineUI(isActive);
            ItemToExamine.SetActive(isActive);
            LightSource.SetActive(isActive);
        }

    }

}

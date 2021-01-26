using UnityEngine;
using FirstPerson.UI;

namespace FirstPerson.Examine
{

    public class PlayerExamine : MonoBehaviour
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

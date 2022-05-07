using UnityEngine;
using FirstPerson.PlayerController;
using FirstPerson.Inventory;
using FirstPerson.Crafting;
using FirstPerson.Examine;

namespace FirstPerson.UI
{

    public class UIPanels : MonoBehaviour
    {

        [SerializeField] GameObject inventoryPanel = null;
        [SerializeField] GameObject containerPanel = null;
        [SerializeField] GameObject craftingPanel = null;
        [SerializeField] GameObject examinePanel = null;

        private static GameObject[] panels;


        #region Properties
        public static bool IsOnScreen
        {
            get
            {
                foreach (GameObject panel in panels)
                {
                    if (panel.activeSelf)
                    {
                        return true;
                    }
                }
                return false;
            }
        }
        #endregion


        private void Awake()
        {
            SetReferences();
        }

        private void Start()
        {
            CloseAllPanels();
        }

        private void Update()
        {
            if (Input.GetButtonDown(Control.Inventory))
            {
                ToggleUIPanel(PlayerInventory.Panel);
            }
        }

        public static void ToggleUIPanel(GameObject panel)
        {
            if (!PlayerExamine.Panel.activeSelf && !PlayerMovement.IsJumping)
            {
                panel.SetActive(!panel.activeSelf);
                PlayerController.Player.LockCursor(!IsOnScreen);
                PlayerController.Player.LockIntoPlace(IsOnScreen);
            }
        }

        public static void ToggleExamineUI(bool isActive)
        {
            CloseOpenPanels();
            PlayerController.Player.LockCursor(!isActive);
            PlayerController.Player.LockIntoPlace(isActive);
            PlayerExamine.Panel.SetActive(isActive);
        }

        public static void CloseOpenPanels()
        {
            foreach (GameObject panel in panels)
            {
                if (panel.activeSelf)
                {
                    panel.SetActive(false);
                }
            }
        }

        public static void CloseAllPanels()
        {
            foreach (GameObject panel in panels)
            {
                panel.SetActive(false);
            }
        }

        private void SetReferences()
        {
            PlayerInventory.Panel = inventoryPanel;
            Container.Panel = containerPanel;
            CraftItems.Panel = craftingPanel;
            PlayerExamine.Panel = examinePanel;

            panels = new GameObject[] { PlayerInventory.Panel, Container.Panel, CraftItems.Panel, PlayerExamine.Panel };
        }

    }

}

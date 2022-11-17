using An01malia.FirstPerson.Crafting;
using An01malia.FirstPerson.Inspection;
using An01malia.FirstPerson.Inventory;
using UnityEngine;

namespace An01malia.FirstPerson.UI
{
    public class UIPanels : MonoBehaviour
    {
        [SerializeField] private GameObject inventoryPanel = null;
        [SerializeField] private GameObject containerPanel = null;
        [SerializeField] private GameObject craftingPanel = null;
        [SerializeField] private GameObject examinePanel = null;

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

        public static void ToggleUIPanel(GameObject panel)
        {
            if (!PlayerItemInspection.Panel.activeSelf/* && !PlayerMovement.IsJumping*/)
            {
                panel.SetActive(!panel.activeSelf);
                //PlayerController.Player.LockCursor(!IsOnScreen);
                //PlayerController.Player.LockIntoPlace(IsOnScreen);
            }
        }

        public static void ToggleExamineUI(bool isActive)
        {
            CloseOpenPanels();
            //PlayerController.Player.LockCursor(!isActive);
            //PlayerController.Player.LockIntoPlace(isActive);
            PlayerItemInspection.Panel.SetActive(isActive);
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
            PlayerItemInspection.Panel = examinePanel;

            panels = new GameObject[] { PlayerInventory.Panel, Container.Panel, CraftItems.Panel, PlayerItemInspection.Panel };
        }
    }
}
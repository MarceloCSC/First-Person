using An01malia.FirstPerson.Crafting;
using An01malia.FirstPerson.Inspection;
using An01malia.FirstPerson.InventoryModule;
using UnityEngine;

namespace An01malia.FirstPerson.UIModule
{
    public class UIPanels : MonoBehaviour
    {
        #region Fields

        [SerializeField] private GameObject _inventoryPanel;
        [SerializeField] private GameObject _containerPanel;
        [SerializeField] private GameObject _craftingPanel;
        [SerializeField] private GameObject _inspectionPanel;

        private static GameObject[] _panels;

        #endregion

        #region Properties

        public static bool IsOnScreen
        {
            get
            {
                foreach (GameObject panel in _panels)
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

        #region Unity Methods

        private void Awake()
        {
            SetReferences();
        }

        private void Start()
        {
            CloseAllPanels();
        }

        #endregion

        #region Public Methods

        public static void ToggleUIPanel(GameObject panel)
        {
            if (!PlayerItemInspection.Panel.activeSelf)
            {
                panel.SetActive(!panel.activeSelf);
            }
        }

        public static void ToggleInspectionUI(bool isActive)
        {
            CloseOpenPanels();

            PlayerItemInspection.Panel.SetActive(isActive);
        }

        public static void CloseOpenPanels()
        {
            foreach (GameObject panel in _panels)
            {
                if (panel.activeSelf)
                {
                    panel.SetActive(false);
                }
            }
        }

        public static void CloseAllPanels()
        {
            foreach (GameObject panel in _panels)
            {
                panel.SetActive(false);
            }
        }

        #endregion

        #region Private Methods

        private void SetReferences()
        {
            PlayerInventory.Panel = _inventoryPanel;
            Container.Panel = _containerPanel;
            CraftItems.Panel = _craftingPanel;
            PlayerItemInspection.Panel = _inspectionPanel;

            _panels = new GameObject[] { PlayerInventory.Panel, Container.Panel, CraftItems.Panel, PlayerItemInspection.Panel };
        }

        #endregion
    }
}
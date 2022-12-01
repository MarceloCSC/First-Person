using An01malia.FirstPerson.Inspection;
using An01malia.FirstPerson.InventoryModule;
using UnityEngine;

namespace An01malia.FirstPerson.UIModule
{
    public class UIPanelManager : MonoBehaviour
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
                    if (panel.activeSelf) return true;
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
            if (!PlayerInspection.Panel.activeSelf)
            {
                panel.SetActive(!panel.activeSelf);

                Cursor.lockState = panel.activeSelf ? CursorLockMode.None : CursorLockMode.Locked;
                Cursor.visible = panel.activeSelf;
            }
        }

        public static void ToggleInspectionUI(bool isActive)
        {
            CloseOpenPanels();

            PlayerInspection.Panel.SetActive(isActive);
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
            PlayerInspection.Panel = _inspectionPanel;

            _panels = new GameObject[] { PlayerInventory.Panel, Container.Panel, PlayerInspection.Panel };
        }

        #endregion
    }
}
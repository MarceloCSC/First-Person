using An01malia.FirstPerson.DialogueModule;
using An01malia.FirstPerson.InventoryModule;
using An01malia.FirstPerson.PlayerModule.States;
using UnityEngine;

namespace An01malia.FirstPerson.UserInterfaceModule
{
    public class UIPanelManager : MonoBehaviour
    {
        #region Fields

        [SerializeField] private GameObject _inventoryPanel;
        [SerializeField] private GameObject _containerPanel;
        [SerializeField] private GameObject _craftingPanel;
        [SerializeField] private GameObject _inspectionPanel;
        [SerializeField] private GameObject _dialoguePanel;

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
            panel.SetActive(!panel.activeSelf);

            Cursor.lockState = panel.activeSelf ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = panel.activeSelf;
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
            PlayerInspectState.Panel = _inspectionPanel;
            DialogueManager.Panel = _dialoguePanel;

            _panels = new GameObject[]
            {
                PlayerInventory.Panel,
                Container.Panel,
                PlayerInspectState.Panel,
                DialogueManager.Panel
            };
        }

        #endregion
    }
}
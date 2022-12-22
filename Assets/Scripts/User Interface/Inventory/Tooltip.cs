using An01malia.FirstPerson.Core;
using An01malia.FirstPerson.InventoryModule;
using An01malia.FirstPerson.ItemModule;
using An01malia.FirstPerson.ItemModule.Items;
using An01malia.FirstPerson.PlayerModule;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace An01malia.FirstPerson.UserInterfaceModule.Inventory
{
    public class Tooltip : MonoBehaviour, IPointerExitHandler
    {
        #region Fields

        public Button ExamineButton, UseButton, EquipButton, DropButton;

        [Header("Screen Positioning")]
        [SerializeField] private Canvas _inventoryCanvas;
        [SerializeField] private float _padding = 5.0f;

        private Button[] _buttons;
        private RectTransform _rectTransform;
        private PlayerInput _playerInput;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            SetReferences();

            gameObject.SetActive(false);
        }

        #endregion

        #region Public Methods

        public void ShowTooltip(InventoryItem item)
        {
            Vector3 cursorPosition = _playerInput.CursorInputValues;
            transform.position = cursorPosition.GetPositionOnScreen(_inventoryCanvas, _rectTransform, _padding);

            RestoreButtons();
            GetItemType(item);

            gameObject.SetActive(true);
        }

        public void HideTooltip()
        {
            if (!gameObject.activeSelf) return;

            Slot.ItemSelected = null;
            gameObject.SetActive(false);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!eventData.pointerCurrentRaycast.isValid || eventData.pointerCurrentRaycast.gameObject.transform.parent != transform)
            {
                HideTooltip();
            }
        }

        #endregion

        #region Private Methods

        private void GetItemType(InventoryItem item)
        {
            switch (item.Root.Type)
            {
                case ItemType.Food:
                    Deactivate(EquipButton);
                    break;

                case ItemType.Valuable:
                    Deactivate(UseButton);
                    break;

                case ItemType.Readable:
                    Deactivate(UseButton);
                    Deactivate(EquipButton);
                    break;

                default:
                    break;
            }
        }

        private void Deactivate(Button button)
        {
            button.interactable = false;
            button.GetComponentInChildren<Text>().color = Color.grey;
        }

        private void RestoreButtons()
        {
            foreach (Button button in _buttons)
            {
                button.interactable = true;
                button.GetComponentInChildren<Text>().color = Color.white;
            }
        }

        private void SetReferences()
        {
            _buttons = new Button[] { ExamineButton, UseButton, EquipButton, DropButton };
            _rectTransform = GetComponent<RectTransform>();

            // MUDAR!!
            _playerInput = FindObjectOfType<PlayerInput>();
        }

        #endregion
    }
}
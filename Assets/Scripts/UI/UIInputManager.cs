using An01malia.FirstPerson.Inventory;
using An01malia.FirstPerson.Player;
using UnityEngine;

namespace An01malia.FirstPerson.UI
{
    public class UIInputManager : MonoBehaviour
    {
        #region Fields

        private Vector2 _cursorInputValues;

        private InputActions _actions;
        private PlayerController _playerController;

        #endregion

        #region Properties

        public Vector2 CursorInputValues => _cursorInputValues;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            _actions = new InputActions();
            // MUDAR!!
            _playerController = FindObjectOfType<PlayerController>();
        }

        private void OnEnable()
        {
            _actions.UI.Enable();
            _actions.UI.Inventory.performed += _ => {
                _playerController.LockCursor(!UIPanels.IsOnScreen);
                _playerController.LockIntoPlace(UIPanels.IsOnScreen);
                UIPanels.ToggleUIPanel(PlayerInventory.Panel); 
            };
        }

        private void OnDisable()
        {
            _actions.UI.Disable();
            _actions.UI.Inventory.performed -= _ => UIPanels.ToggleUIPanel(PlayerInventory.Panel);
        }

        private void Update()
        {
            _cursorInputValues = _actions.UI.Cursor.ReadValue<Vector2>();
        }

        #endregion
    }
}
using An01malia.FirstPerson.Core;
using An01malia.FirstPerson.Core.References;
using An01malia.FirstPerson.InteractionModule;
using An01malia.FirstPerson.PlayerModule.States.DTOs;
using UnityEngine;
using UnityEngine.InputSystem;

namespace An01malia.FirstPerson.PlayerModule
{
    public enum ActionType
    {
        None,
        Run,
        Crouch,
        Jump,
        Push,
        Climb,
        Carry,
        Interact,
        GrabLedge,
        Inventory
    }

    public class PlayerInput : MonoBehaviour
    {
        #region Fields

        private Vector2 _movementInputValues;
        private Vector2 _viewInputValues;
        private Vector2 _cursorInputValues;

        private InputActions _actions;
        private PlayerController _context;
        private PlayerInteraction _interaction;
        private PlayerSurroundings _surroundings;

        #endregion

        #region Properties

        public Vector2 MovementInputValues => _movementInputValues;
        public Vector2 ViewInputValues => _viewInputValues;
        public Vector2 CursorInputValues => _cursorInputValues;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            SetReferences();
        }

        private void OnEnable()
        {
            EnableInputActions();
            SubscribeEvents();
        }

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
            DisableInputActions();
        }

        #endregion

        #region Public Methods

        public void UpdateInput()
        {
            _movementInputValues = _actions.Player.Movement.ReadValue<Vector2>();
            _viewInputValues = _actions.Player.View.ReadValue<Vector2>();
            _cursorInputValues = _actions.UI.Cursor.ReadValue<Vector2>();
        }

        #endregion

        #region Private Methods

        #region Input Actions

        private void OnJumpPressed(InputAction.CallbackContext callback)
        {
            if (_surroundings.CanGrabLedge)
            {
                _context.CurrentState.TriggerSwitchState(ActionType.GrabLedge);
            }
            else
            {
                _context.CurrentState.TriggerSwitchState(ActionType.Jump);
            }
        }

        private void OnRunPressed(InputAction.CallbackContext callback)
        {
            _context.CurrentState.TriggerSwitchState(ActionType.Run,
                                                     new RunActionDTO(callback.phase == InputActionPhase.Performed));
        }

        private void OnCrouchPressed(InputAction.CallbackContext callback)
        {
            _context.CurrentState.TriggerSwitchState(ActionType.Crouch);
        }

        private void OnInteractionPressed(InputAction.CallbackContext callback)
        {
            if (_interaction.TryGetInteractive(out IInteractive interactive))
            {
                var item = _interaction.InteractiveItem;

                switch (LayerMask.LayerToName(item.gameObject.layer))
                {
                    case Layer.Carriable:
                        _context.CurrentState.TriggerSwitchState(ActionType.Carry, new ItemActionDTO(item));
                        break;

                    case Layer.Pushable:
                        _context.CurrentState.TriggerSwitchState(ActionType.Push, new ItemActionDTO(item));
                        break;

                    case Layer.Climbable:
                        _context.CurrentState.TriggerSwitchState(ActionType.Climb, new ItemActionDTO(item));
                        break;

                    case Layer.Interactive:
                        _context.CurrentState.TriggerSwitchState(ActionType.Interact, new InteractiveActionDTO(interactive));
                        break;
                }
            }
            else
            {
                _context.CurrentState.TriggerSwitchState(ActionType.None);
            }
        }

        private void OnInventoryPressed(InputAction.CallbackContext callback)
        {
            _context.CurrentState.TriggerSwitchState(ActionType.Inventory);
        }

        private void OnPausePressed(InputAction.CallbackContext callback)
        {
            GameStateManager.Instance.ChangeState(GameState.Paused);
        }

        #endregion

        private void OnGameStateChanged(GameState gameState)
        {
            switch (gameState)
            {
                case GameState.Inventory:
                    if (_actions.Player.enabled) _actions.Player.Disable();
                    break;

                case GameState.Paused:
                    DisableInputActions();
                    break;

                case GameState.Gameplay:
                    EnableInputActions();
                    break;
            }
        }

        private void EnableInputActions()
        {
            if (!_actions.Game.enabled) _actions.Game.Enable();
            if (!_actions.Player.enabled) _actions.Player.Enable();
            if (!_actions.Inspection.enabled) _actions.Inspection.Enable();
            if (!_actions.UI.enabled) _actions.UI.Enable();
        }

        private void DisableInputActions()
        {
            if (_actions.Player.enabled) _actions.Player.Disable();
            if (_actions.Inspection.enabled) _actions.Inspection.Disable();
            if (_actions.UI.enabled) _actions.UI.Disable();
        }

        private void SubscribeEvents()
        {
            _actions.Game.Pause.performed += OnPausePressed;
            _actions.Player.Jump.performed += OnJumpPressed;
            _actions.Player.Run.performed += OnRunPressed;
            _actions.Player.Run.canceled += OnRunPressed;
            _actions.Player.Crouch.performed += OnCrouchPressed;
            _actions.Player.Interact.performed += OnInteractionPressed;
            _actions.UI.Inventory.performed += OnInventoryPressed;
            GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;
        }

        private void UnsubscribeEvents()
        {
            _actions.Game.Pause.performed -= OnPausePressed;
            _actions.Player.Jump.performed -= OnJumpPressed;
            _actions.Player.Run.performed -= OnRunPressed;
            _actions.Player.Run.canceled -= OnRunPressed;
            _actions.Player.Crouch.performed -= OnCrouchPressed;
            _actions.Player.Interact.performed -= OnInteractionPressed;
            _actions.UI.Inventory.performed -= OnInventoryPressed;

            if (GameStateManager.Instance != null)
            {
                GameStateManager.Instance.OnGameStateChanged -= OnGameStateChanged;
            }
        }

        private void SetReferences()
        {
            _actions = new InputActions();
            _context = GetComponent<PlayerController>();
            _interaction = GetComponent<PlayerInteraction>();
            _surroundings = GetComponent<PlayerSurroundings>();
        }

        #endregion
    }
}
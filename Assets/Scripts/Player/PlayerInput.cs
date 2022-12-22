using An01malia.FirstPerson.Core;
using An01malia.FirstPerson.Core.References;
using An01malia.FirstPerson.InteractionModule.Interactive;
using An01malia.FirstPerson.ItemModule.Items;
using An01malia.FirstPerson.PlayerModule.States;
using An01malia.FirstPerson.PlayerModule.States.DTOs;
using UnityEngine;
using UnityEngine.InputSystem;

namespace An01malia.FirstPerson.PlayerModule
{
    public class PlayerInput : MonoBehaviour
    {
        #region Fields

        private bool _isGameplayEnabled;
        private bool _isUIEnabled;
        private Vector2 _movementInputValues;
        private Vector2 _viewInputValues;
        private Vector2 _cursorInputValues;
        private Vector2 _zoomInputValues;
        private Vector2 _rotationInputValues;

        private InputActions _actions;
        private PlayerController _context;
        private PlayerInteraction _interaction;
        private PlayerSurroundings _surroundings;

        #endregion

        #region Properties

        public Vector2 MovementInputValues => _movementInputValues;
        public Vector2 ViewInputValues => _viewInputValues;
        public Vector2 CursorInputValues => _cursorInputValues;
        public Vector2 ZoomInputValues => _zoomInputValues;
        public Vector2 RotationInputValues => _rotationInputValues;

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
            _isGameplayEnabled = true;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        #region Public Methods

        public void UpdateInput()
        {
            if (_isGameplayEnabled) SetGameplayInput();
            else if (_isUIEnabled) SetUIInput();
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
            if (!_interaction.Interaction)
            {
                _context.CurrentState.TriggerSwitchState(ActionType.None);

                return;
            }

            Transform interaction = _interaction.Interaction;

            if (TryHandleInteractive(interaction)) return;

            if (TryHandleItem(interaction)) return;

            HandleInteraction(interaction);
        }

        private void OnInventoryPressed(InputAction.CallbackContext callback)
        {
            _context.CurrentState.TriggerSwitchState(ActionType.Inventory);
        }

        private void OnPausePressed(InputAction.CallbackContext callback)
        {
            GameStateManager.Instance.ChangeState(GameState.Paused);
        }

        private void OnEscapePressed(InputAction.CallbackContext callback)
        {
            if (_isUIEnabled)
            {
                _context.CurrentState.TriggerSwitchState(ActionType.None);

                return;
            }

            GameStateManager.Instance.ChangeState(GameState.Paused);
            // TOGGLE GAME MENU
        }

        #endregion

        private bool TryHandleInteractive(Transform interaction)
        {
            if (!_interaction.TryGetInteractive(out IInteractive interactive)) return false;

            switch (interactive)
            {
                case StorageUnit:
                    _context.CurrentState.TriggerSwitchState(ActionType.Inventory, new TransformActionDTO(interaction));
                    break;

                case NPC:
                    _context.CurrentState.TriggerSwitchState(ActionType.Dialogue, new TransformActionDTO(interaction));
                    break;

                default:
                    _context.CurrentState.TriggerSwitchState(ActionType.Interact, new InteractiveActionDTO(interactive));
                    break;
            }

            return true;
        }

        private bool TryHandleItem(Transform interaction)
        {
            if (!_interaction.TryGetItem(out IItem item)) return false;

            switch (item)
            {
                case ItemToCarry:
                    _context.CurrentState.TriggerSwitchState(ActionType.Carry, new TransformActionDTO(interaction));
                    break;

                case ItemToPickUp:
                    // HANDLE PICKING UP
                    break;

                case ItemToInspect when _context.CurrentState is PlayerInspectState:
                    _context.CurrentState.TriggerSwitchState(ActionType.None);
                    break;

                case ItemToInspect:
                    _context.CurrentState.TriggerSwitchState(ActionType.Inspect, new TransformActionDTO(interaction));
                    break;
            }

            return true;
        }

        private void HandleInteraction(Transform interaction)
        {
            switch (LayerMask.LayerToName(interaction.gameObject.layer))
            {
                case Layer.ToPush:
                    _context.CurrentState.TriggerSwitchState(ActionType.Push, new TransformActionDTO(interaction));
                    break;

                case Layer.ToClimb:
                    _context.CurrentState.TriggerSwitchState(ActionType.Climb, new TransformActionDTO(interaction));
                    break;

                default:
                    break;
            }
        }

        private void OnGameStateChanged(GameState gameState)
        {
            switch (gameState)
            {
                case GameState.Gameplay:
                    EnableInputActions();
                    ToggleInputReading(true, false);
                    break;

                case GameState.Paused:
                    DisableInputActions();
                    ToggleInputReading(false, false);
                    break;

                case GameState.UI:
                    EnableInputActions();
                    ToggleInputReading(false, true);
                    break;
            }
        }

        private void SetGameplayInput()
        {
            _movementInputValues = _actions.Player.Movement.ReadValue<Vector2>();
            _viewInputValues = _actions.Player.View.ReadValue<Vector2>();
        }

        private void SetUIInput()
        {
            _cursorInputValues = _actions.UI.Cursor.ReadValue<Vector2>();
            _zoomInputValues = _actions.UI.Zoom.ReadValue<Vector2>();

            if (_actions.UI.Hold.IsPressed())
            {
                _rotationInputValues = _actions.UI.Rotate.ReadValue<Vector2>();
            }
        }

        private void ToggleInputReading(bool isGameplayEnabled, bool isUIEnabled)
        {
            _isGameplayEnabled = isGameplayEnabled;
            _isUIEnabled = isUIEnabled;
        }

        private void EnableInputActions()
        {
            if (!_actions.Game.enabled) _actions.Game.Enable();
            if (!_actions.Player.enabled) _actions.Player.Enable();
            if (!_actions.UI.enabled) _actions.UI.Enable();
        }

        private void DisableInputActions()
        {
            _actions.Player.Disable();
            _actions.UI.Disable();
        }

        private void SubscribeEvents()
        {
            _actions.Player.Jump.performed += OnJumpPressed;
            _actions.Player.Run.performed += OnRunPressed;
            _actions.Player.Run.canceled += OnRunPressed;
            _actions.Player.Crouch.performed += OnCrouchPressed;
            _actions.Player.Interact.performed += OnInteractionPressed;
            _actions.UI.Inventory.performed += OnInventoryPressed;
            _actions.Game.Pause.performed += OnPausePressed;
            _actions.Game.Escape.performed += OnEscapePressed;

            GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;
        }

        private void UnsubscribeEvents()
        {
            _actions.Player.Jump.performed -= OnJumpPressed;
            _actions.Player.Run.performed -= OnRunPressed;
            _actions.Player.Run.canceled -= OnRunPressed;
            _actions.Player.Crouch.performed -= OnCrouchPressed;
            _actions.Player.Interact.performed -= OnInteractionPressed;
            _actions.UI.Inventory.performed -= OnInventoryPressed;
            _actions.Game.Pause.performed -= OnPausePressed;
            _actions.Game.Escape.performed -= OnEscapePressed;

            if (GameStateManager.Instance != null) GameStateManager.Instance.OnGameStateChanged -= OnGameStateChanged;
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
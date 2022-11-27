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
        Climb,
        Push,
        Carry,
        GrabLedge,
        Interact,
        Inventory
    }

    public class PlayerInput : MonoBehaviour
    {
        #region Fields

        [Header("Grab Ledge")]
        [SerializeField] private float _rayLength = 0.75f;
        [SerializeField] private Vector3 _upperBounds = new(0.0f, 0.45f, 0.0f);
        [SerializeField] private Vector3 _lowerBounds = new(0.0f, 0.9f, 0.0f);
        [SerializeField] private LayerMask _layersToGrab;

        private Vector2 _movementInputValues;
        private Vector2 _viewInputValues;
        private Vector2 _cursorInputValues;

        private InputActions _actions;
        private PlayerController _context;
        private PlayerInteraction _interaction;

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
            SubscribeToInputs();
        }

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void OnDisable()
        {
            UnsubscribeToInputs();
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

        #region Input Actions

        private void OnJumpPressed(InputAction.CallbackContext callback)
        {
            if (Physics.Raycast(transform.position - _lowerBounds, transform.forward, _rayLength, _layersToGrab) &&
                !Physics.Raycast(transform.position + _upperBounds, transform.forward, _rayLength, _layersToGrab))
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
            if (_interaction.TrySetInteractive(out IInteractive interactive))
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

        #endregion

        #region Private Methods

        private void SetReferences()
        {
            _actions = new InputActions();
            _context = GetComponent<PlayerController>();
            _interaction = GetComponent<PlayerInteraction>();
        }

        private void SubscribeToInputs()
        {
            _actions.Player.Enable();
            _actions.Player.Jump.performed += OnJumpPressed;
            _actions.Player.Run.performed += OnRunPressed;
            _actions.Player.Run.canceled += OnRunPressed;
            _actions.Player.Crouch.performed += OnCrouchPressed;
            _actions.Player.Interact.performed += OnInteractionPressed;

            _actions.UI.Enable();
            _actions.UI.Inventory.performed += OnInventoryPressed;

            _actions.Inspection.Enable();
        }

        private void UnsubscribeToInputs()
        {
            _actions.Player.Disable();
            _actions.Player.Jump.performed -= OnJumpPressed;
            _actions.Player.Run.performed -= OnRunPressed;
            _actions.Player.Run.canceled -= OnRunPressed;
            _actions.Player.Crouch.performed -= OnCrouchPressed;
            _actions.Player.Interact.performed -= OnInteractionPressed;

            _actions.UI.Disable();
            _actions.UI.Inventory.performed -= OnInventoryPressed;

            _actions.Inspection.Disable();
        }

        #endregion
    }
}
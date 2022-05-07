using UnityEngine;
using UnityEngine.InputSystem;

namespace FirstPerson.Player
{
    public enum ActionType
    {
        None,
        Run,
        Crouch,
        Jump,
        Climb,
        Push,
        PickUp,
        ClimbUpLedge,
        Interact
    }

    public class PlayerInputManager : MonoBehaviour
    {
        #region Fields

        [Header("Climb Up Ledge")]
        [SerializeField] private float _rayLength = 0.75f;
        [SerializeField] private Vector3 _upperBounds = new Vector3(0.0f, 0.45f, 0.0f);
        [SerializeField] private Vector3 _lowerBounds = new Vector3(0.0f, 0.9f, 0.0f);
        [SerializeField] private LayerMask _layersToClimbUp;

        private Vector2 _movementInputValues;
        private Vector2 _cameraInputValues;

        private InputActions _actions;
        private PlayerController _context;
        private PlayerExamine _playerExamine;

        #endregion

        #region Properties

        public Vector2 MovementInputValues => _movementInputValues;
        public Vector2 CameraInputValues => _cameraInputValues;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            _actions = new InputActions();
            _context = GetComponent<PlayerController>();
            _playerExamine = GetComponent<PlayerExamine>();
        }

        private void OnEnable()
        {
            _actions.Player.Enable();
            _actions.Player.Jump.performed += OnJumpPressed;
            _actions.Player.Run.performed += OnRunPressed;
            _actions.Player.Run.canceled += OnRunPressed;
            _actions.Player.Crouch.performed += OnCrouchPressed;
            _actions.Player.Interact.performed += OnInteractionPressed;
        }

        private void OnDisable()
        {
            _actions.Player.Disable();
            _actions.Player.Jump.performed -= OnJumpPressed;
            _actions.Player.Run.performed -= OnRunPressed;
            _actions.Player.Run.canceled -= OnRunPressed;
            _actions.Player.Crouch.performed -= OnCrouchPressed;
            _actions.Player.Interact.performed -= OnInteractionPressed;
        }

        #endregion

        #region Public Methods

        public void UpdateInput()
        {
            _movementInputValues = _actions.Player.Movement.ReadValue<Vector2>();
            _cameraInputValues = _actions.Player.Look.ReadValue<Vector2>();
        }

        #endregion

        #region Input Actions

        private void OnJumpPressed(InputAction.CallbackContext callback)
        {
            if (Physics.Raycast(transform.position - _lowerBounds, transform.forward, _rayLength, _layersToClimbUp) &&
                !Physics.Raycast(transform.position + _upperBounds, transform.forward, _rayLength, _layersToClimbUp))
            {
                _context.CurrentState.TrySwitchState(ActionType.ClimbUpLedge);
            }
            else
            {
                _context.CurrentState.TrySwitchState(ActionType.Jump);
            }
        }

        private void OnRunPressed(InputAction.CallbackContext callback)
        {
            _context.CurrentState.TrySwitchState(ActionType.Run);
            _context.IsRunPressed = callback.phase == InputActionPhase.Performed;
        }

        private void OnCrouchPressed(InputAction.CallbackContext callback)
        {
            _context.CurrentState.TrySwitchState(ActionType.Crouch);
        }

        private void OnInteractionPressed(InputAction.CallbackContext callback)
        {
            if (_playerExamine.CanInteract)
            {
                _context.InteractiveItem = _playerExamine.ExaminedItem;

                if (_context.InteractiveItem.gameObject.layer == LayerMask.NameToLayer("Pickable"))
                {
                    _context.CurrentState.TrySwitchState(ActionType.PickUp);
                }
                else if (_context.InteractiveItem.gameObject.layer == LayerMask.NameToLayer("Pushable"))
                {
                    _context.CurrentState.TrySwitchState(ActionType.Push);
                }
                else if (_context.InteractiveItem.gameObject.layer == LayerMask.NameToLayer("Climbable"))
                {
                    _context.CurrentState.TrySwitchState(ActionType.Climb);
                }
                else if (_context.InteractiveItem.gameObject.layer == LayerMask.NameToLayer("Interactive"))
                {
                    _context.CurrentState.TrySwitchState(ActionType.Interact);
                }
            }
            else
            {
                _context.CurrentState.TrySwitchState(ActionType.None);
            }
        }

        #endregion
    }
}
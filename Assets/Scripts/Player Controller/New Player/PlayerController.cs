using FirstPerson.Player.States;
using UnityEngine;

namespace FirstPerson.Player
{
    public class PlayerController : MonoBehaviour
    {
        #region Fields

        private bool _isInputEnabled;
        private bool _isMovementEnabled;
        private bool _isCameraEnabled;
        private bool _isRunPressed;
        private int _jumpsRemaining;
        private float _movementSpeed;
        private Vector3 _momentum;
        private Transform _interactiveItem;
        private PlayerBaseState _currentState;

        private CharacterController _characterController;
        private PlayerStateMachine _stateMachine;
        private PlayerInputManager _inputManager;
        private PlayerCamera _playerCamera;
        private PlayerExamine _playerExamine;

        #endregion

        #region Properties

        public bool IsRunPressed { get => _isRunPressed; set => _isRunPressed = value; }
        public int JumpsRemaining { get => _jumpsRemaining; set => _jumpsRemaining = value; }
        public float MovementSpeed { get => _movementSpeed; set => _movementSpeed = value; }
        public Vector3 Momentum { get => _momentum; set => _momentum = value; }
        public Transform InteractiveItem { get => _interactiveItem; set => _interactiveItem = value; }
        public PlayerBaseState CurrentState { get => _currentState; set => _currentState = value; }

        #endregion

        #region Unity Methods

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
            _stateMachine = GetComponent<PlayerStateMachine>();
            _inputManager = GetComponent<PlayerInputManager>();
            _playerCamera = GetComponent<PlayerCamera>();
            _playerExamine = GetComponent<PlayerExamine>();
        }

        private void Start()
        {
            _characterController.minMoveDistance = 0.0f;
            _currentState = _stateMachine.Idle();
            _currentState.EnterState();
            _isInputEnabled = true;
            _isMovementEnabled = true;
            _isCameraEnabled = true;
        }

        private void Update()
        {
            if (_isInputEnabled)
            {
                _inputManager.UpdateInput();
            }
        }

        private void FixedUpdate()
        {
            if (_isMovementEnabled)
            {
                _currentState.UpdateStates();
            }
            _playerExamine.Examine();
        }

        private void LateUpdate()
        {
            if (_isCameraEnabled)
            {
                _playerCamera.UpdateCamera();
            }
        }

        #endregion
    }
}
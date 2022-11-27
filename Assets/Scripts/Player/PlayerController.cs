using An01malia.FirstPerson.PlayerModule.States;
using An01malia.FirstPerson.PlayerModule.States.DTOs;
using UnityEngine;

namespace An01malia.FirstPerson.PlayerModule
{
    public class PlayerController : MonoBehaviour
    {
        #region Fields

        private PlayerStateMachine _stateMachine;
        private PlayerInput _playerInput;
        private PlayerCamera _playerCamera;
        private PlayerInteraction _interaction;

        #endregion

        #region Properties

        public bool IsInputEnabled { get; set; }
        public bool IsMovementEnabled { get; set; }
        public bool IsCameraEnabled { get; set; }
        public PlayerBaseState CurrentState { get; private set; }

        #endregion

        #region Unity Methods

        private void Awake()
        {
            SetReferences();
        }

        private void Start()
        {
            CurrentState = _stateMachine.Idle();
            CurrentState.EnterState(new PlayerActionDTO(0.0f, false, Vector3.zero));

            IsInputEnabled = true;
            IsMovementEnabled = true;
            IsCameraEnabled = true;
        }

        private void Update()
        {
            if (!IsInputEnabled) return;

            _playerInput.UpdateInput();
        }

        private void FixedUpdate()
        {
            _interaction.Examine();

            if (!IsMovementEnabled) return;

            CurrentState.UpdateStates();
        }

        private void LateUpdate()
        {
            if (!IsCameraEnabled) return;

            _playerCamera.UpdateCamera();
        }

        #endregion

        #region Public Methods

        public void SetCurrentState(PlayerBaseState newState)
        {
            CurrentState = newState;
        }

        #endregion

        #region Private Methods

        private void SetReferences()
        {
            _stateMachine = GetComponentInChildren<PlayerStateMachine>();
            _playerInput = GetComponent<PlayerInput>();
            _playerCamera = GetComponent<PlayerCamera>();
            _interaction = GetComponent<PlayerInteraction>();
        }

        #endregion
    }
}
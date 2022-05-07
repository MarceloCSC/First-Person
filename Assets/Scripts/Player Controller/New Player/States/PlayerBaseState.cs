using UnityEngine;

namespace FirstPerson.Player.States
{
    public abstract class PlayerBaseState : MonoBehaviour
    {
        #region Fields

        private PlayerBaseState _currentSuperState;
        private PlayerBaseState _currentSubState;

        protected CharacterController _characterController;
        protected PlayerController _context;
        protected PlayerStateMachine _stateMachine;
        protected PlayerInputManager _inputManager;
        protected PlayerCamera _camera;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
            _context = GetComponent<PlayerController>();
            _stateMachine = GetComponent<PlayerStateMachine>();
            _inputManager = GetComponent<PlayerInputManager>();
            _camera = GetComponent<PlayerCamera>();
        }

        #endregion

        #region Abstract Methods

        public abstract void EnterState();

        public abstract void UpdateState();

        public abstract void ExitState();

        public abstract void CheckSwitchState();

        public abstract bool TrySwitchState(ActionType action);

        #endregion

        #region Public Methods

        public void UpdateStates()
        {
            UpdateState();

            if (_currentSubState)
            {
                _currentSubState.UpdateStates();
            }
        }

        #endregion

        #region Protected Methods

        protected void SwitchState(PlayerBaseState newState)
        {
            ExitState();

            newState.EnterState();

            _context.CurrentState = newState;

            if (_currentSubState)
            {
                _context.CurrentState.SetSubState(_currentSubState);
                _currentSubState = null;
            }
        }

        protected void SwitchSubState(PlayerBaseState newSubState)
        {
            if (_currentSubState)
            {
                _currentSubState.ExitState();
            }

            SetSubState(newSubState);

            _currentSubState.EnterState();
        }

        protected bool TrySwitchSubState(ActionType action)
        {
            if (_currentSubState)
            {
                _currentSubState.TrySwitchState(action);
                return true;
            }

            return false;
        }

        protected void RemoveSubState()
        {
            if (_currentSuperState)
            {
                _currentSuperState.RemoveSubState();
                _currentSuperState = null;
            }
            else
            {
                _currentSubState.ExitState();
                _currentSubState = null;
            }
        }

        #endregion

        #region Private Methods

        private void SetSuperState(PlayerBaseState newSuperState)
        {
            _currentSuperState = newSuperState;
        }

        private void SetSubState(PlayerBaseState newSubState)
        {
            _currentSubState = newSubState;

            newSubState.SetSuperState(this);
        }

        #endregion
    }
}
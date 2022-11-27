using An01malia.FirstPerson.PlayerModule.States.Data;
using An01malia.FirstPerson.PlayerModule.States.DTOs;
using UnityEngine;

namespace An01malia.FirstPerson.PlayerModule.States
{
    public abstract class PlayerBaseState : MonoBehaviour
    {
        #region Fields

        protected PlayerBaseState SuperState;
        protected PlayerBaseState SubState;

        protected CharacterController Controller;
        protected PlayerStateMachine StateMachine;
        protected PlayerInput Input;

        private PlayerController _context;

        #endregion

        #region Properties

        protected PlayerStateData StateData { get; set; }

        #endregion

        #region Unity Methods

        private void Awake()
        {
            SetReferences();
        }

        #endregion

        #region Abstract Methods

        public abstract void EnterState(PlayerActionDTO dto = null);

        public abstract void UpdateState();

        public abstract PlayerActionDTO ExitState();

        public abstract void CheckSwitchState();

        #endregion

        #region Public Methods

        public void UpdateStates()
        {
            UpdateState();

            if (!SubState) return;

            SubState.UpdateStates();
        }

        public virtual void TriggerSwitchState(ActionType action, ActionDTO dto = null)
        {
            if (!SubState) return;

            SubState.TriggerSwitchState(action, dto);
        }

        public void RemoveSubState()
        {
            SubState.SuperState = null;
            SwitchState(this, null);
        }

        #endregion

        #region Protected Methods

        protected void SwitchState(PlayerBaseState newState)
        {
            if (newState == _context.CurrentState) return;

            if (SubState)
            {
                newState.SetSubState(SubState);
                SubState = null;
            }

            var actionDto = ExitState();

            newState.EnterState(actionDto);

            _context.SetCurrentState(newState);
        }

        protected void SwitchState(PlayerBaseState newState, PlayerBaseState newSubState)
        {
            SwitchState(newState);
            SetSubState(newSubState);
        }

        #endregion

        #region Private Methods

        protected void SetSubState(PlayerBaseState newSubState)
        {
            if (SubState && SubState != newSubState)
            {
                SubState.ExitState();
                SubState.SuperState = null;
            }

            SubState = newSubState;

            if (!newSubState) return;

            if (!SubState.SuperState)
            {
                SubState.EnterState(StateData.GetData());
            }

            SubState.SetSuperState(this);
        }

        protected void SetSuperState(PlayerBaseState newSuperState)
        {
            SuperState = newSuperState;
        }

        private void SetReferences()
        {
            Controller = GetComponentInParent<CharacterController>();
            StateMachine = GetComponent<PlayerStateMachine>();
            Input = GetComponentInParent<PlayerInput>();
            _context = GetComponentInParent<PlayerController>();
        }

        #endregion
    }
}
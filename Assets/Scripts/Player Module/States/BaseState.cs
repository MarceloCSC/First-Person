using An01malia.FirstPerson.PlayerModule.States.Data;
using An01malia.FirstPerson.PlayerModule.States.DTOs;
using UnityEngine;

namespace An01malia.FirstPerson.PlayerModule.States
{
    public abstract class BaseState : MonoBehaviour
    {
        #region Fields

        protected BaseState SuperState;
        protected BaseState SubState;

        #endregion

        #region Properties

        protected PlayerStateData StateData { get; set; }

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

        protected virtual void SwitchState(BaseState newState)
        {
            if (SubState)
            {
                newState.SetSubState(SubState);
                SubState = null;
            }

            var actionDto = ExitState();

            newState.EnterState(actionDto);
        }

        protected void SwitchState(BaseState newState, BaseState newSubState)
        {
            SwitchState(newState);
            SetSubState(newSubState);
        }

        protected void SetSubState(BaseState newSubState)
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

        protected void SetSuperState(BaseState newSuperState)
        {
            SuperState = newSuperState;
        }

        #endregion
    }
}
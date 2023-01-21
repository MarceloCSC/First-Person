using An01malia.FirstPerson.PlayerModule.States.Data;
using An01malia.FirstPerson.PlayerModule.States.DTOs;
using System;
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

        public virtual void UpdateStates()
        {
            UpdateState();

            if (!SubState) return;

            SubState.UpdateStates();
        }

        public virtual bool TrySwitchState(ActionType action, ActionDTO dto = null)
        {
            if (!SubState) return false;

            return SubState.TrySwitchState(action, dto);
        }

        public PlayerActionDTO GetData() => StateData.GetData();

        #endregion

        #region Protected Methods

        protected virtual void SwitchState(BaseState newState)
        {
            var actionDto = ExitState();

            newState.EnterState(actionDto);
        }

        protected void SwapState(BaseState newState)
        {
            if (newState == null) throw new NullReferenceException("BaseState 'newState' cannot be null.");

            if (newState == this) return;

            if (SubState)
            {
                newState.SubState = SubState;
                SubState.SuperState = newState;
                SubState = null;
            }

            SwitchState(newState);
        }

        protected void PushState(BaseState newState)
        {
            if (newState == null) throw new NullReferenceException("BaseState 'newState' cannot be null.");

            newState.SubState = this;
            SuperState = newState;

            SwitchState(newState);
        }

        protected void PopState()
        {
            if (!SubState) return;

            SwitchState(SubState);

            SubState.SuperState = null;
            SubState = null;
        }

        protected void AppendState(BaseState newState)
        {
            if (newState == null) throw new NullReferenceException("BaseState 'newState' cannot be null.");

            if (SubState)
            {
                SubState.ExitState();
                SubState.SuperState = null;
            }

            SubState = newState;
            SubState.SuperState = this;

            SubState.EnterState(StateData.GetData());
        }

        protected void RemoveState()
        {
            if (!SuperState) return;

            ExitState();
            SuperState.SubState = null;
            SuperState = null;
        }

        #endregion
    }
}
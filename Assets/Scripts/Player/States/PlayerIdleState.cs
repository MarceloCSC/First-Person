using An01malia.FirstPerson.ItemModule.Items;
using An01malia.FirstPerson.PlayerModule.States.Data;
using An01malia.FirstPerson.PlayerModule.States.DTOs;
using UnityEngine;

namespace An01malia.FirstPerson.PlayerModule.States
{
    public class PlayerIdleState : PlayerBaseState
    {
        #region Fields

        [SerializeField] private float _gravityPull = 10.0f;

        #endregion

        #region Overriden Methods

        public override void EnterState(PlayerActionDTO dto)
        {
            StateData = new PlayerStateData(dto);
        }

        public override PlayerActionDTO ExitState() => StateData.GetData();

        public override void UpdateState()
        {
            HandleGravity();
            CheckSwitchState();
        }

        public override void CheckSwitchState()
        {
            if (!Controller.isGrounded)
            {
                SwapState(StateMachine.Fall());
                return;
            }

            if (HasNoInput()) return;

            if (StateData.IsRunPressed)
            {
                SwapState(StateMachine.Run());
                return;
            }

            SwapState(StateMachine.Walk());
        }

        public override bool TrySwitchState(ActionType action, ActionDTO dto = null)
        {
            if (base.TrySwitchState(action, dto)) return false;

            switch (action)
            {
                case ActionType.Run:
                    StateData.SetData(dto);
                    return false;

                case ActionType.Crouch:
                    SwapState(StateMachine.Crouch());
                    return true;

                case ActionType.Jump:
                    SwapState(StateMachine.Jump());
                    return true;

                case ActionType.GrabLedge:
                    SwapState(StateMachine.GrabLedge());
                    return true;

                case ActionType.Push:
                    StateData.SetData(dto);
                    SwapState(StateMachine.Push());
                    return true;

                case ActionType.Climb:
                    StateData.SetData(dto);
                    SwapState(StateMachine.Climb());
                    return true;

                case ActionType.Carry:
                    StateData.SetData(dto);
                    AppendState(StateMachine.Carry());
                    return true;

                case ActionType.Inspect when dto is TransformActionDTO:
                    StateData.SetData(dto);
                    PushState(StateMachine.Inspect());
                    return true;

                case ActionType.Inventory when dto is TransformActionDTO:
                    StateData.SetData(dto);
                    PushState(StateMachine.Inventory());
                    return true;

                case ActionType.Inventory:
                    PushState(StateMachine.Inventory());
                    return true;

                case ActionType.Interact when dto is InteractiveActionDTO actionDto:
                    actionDto.Interactive.StartInteraction();
                    return false;

                case ActionType.Interact when dto is ItemSpotActionDTO actionDto:
                    if (!actionDto.ItemSpot.Item || actionDto.ItemSpot.IsItemLocked) return false;

                    StateData.SetData(new TransformActionDTO(actionDto.ItemSpot.Item));
                    AppendState(StateMachine.Carry());
                    return true;

                case ActionType.Dialogue:
                    StateData.SetData(dto);
                    PushState(StateMachine.Dialogue());
                    return true;

                default:
                    return false;
            }
        }

        #endregion

        #region Private Methods

        private void HandleGravity()
        {
            Vector3 gravityVector = _gravityPull * Vector3.down;

            Controller.Move(gravityVector * Time.fixedDeltaTime);
        }

        private bool HasNoInput() => PlayerInput.MovementInputValues.y == 0.0f && PlayerInput.MovementInputValues.x == 0.0f;

        #endregion
    }
}
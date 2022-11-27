using An01malia.FirstPerson.InventoryModule;
using An01malia.FirstPerson.PlayerModule.States.Data;
using An01malia.FirstPerson.PlayerModule.States.DTOs;
using An01malia.FirstPerson.UIModule;
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
                SwitchState(StateMachine.Fall());
                return;
            }

            if (HasNoInput()) return;

            if (StateData.IsRunPressed)
            {
                SwitchState(StateMachine.Run());
                return;
            }

            SwitchState(StateMachine.Walk());
        }

        public override void TriggerSwitchState(ActionType action, ActionDTO dto = null)
        {
            base.TriggerSwitchState(action, dto);

            switch (action)
            {
                case ActionType.Jump:
                    SwitchState(StateMachine.Jump());
                    break;

                case ActionType.Run:
                    StateData.SetData(dto);
                    break;

                case ActionType.Crouch:
                    SwitchState(StateMachine.Crouch());
                    break;

                case ActionType.GrabLedge:
                    SwitchState(StateMachine.GrabLedge());
                    break;

                case ActionType.Climb:
                    StateData.SetData(dto);
                    SwitchState(StateMachine.Climb());
                    break;

                case ActionType.Push:
                    StateData.SetData(dto);
                    SwitchState(StateMachine.Push());
                    break;

                case ActionType.Carry:
                    StateData.SetData(dto);
                    SwitchState(this, StateMachine.Carry());
                    break;

                case ActionType.Interact:
                    (dto as InteractiveActionDTO).Interactive.StartInteraction();
                    break;

                case ActionType.Inventory:
                    UIPanels.ToggleUIPanel(PlayerInventory.Panel);
                    break;

                default:
                    break;
            }
        }

        #endregion

        #region Private Methods

        private void HandleGravity()
        {
            Vector3 gravityVector = _gravityPull * Vector3.down;

            Controller.Move(gravityVector * Time.fixedDeltaTime);
        }

        private bool HasNoInput() => Input.MovementInputValues.y == 0.0f && Input.MovementInputValues.x == 0.0f;

        #endregion
    }
}
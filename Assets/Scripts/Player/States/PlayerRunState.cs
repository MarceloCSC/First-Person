using An01malia.FirstPerson.Core.References;
using An01malia.FirstPerson.PlayerModule.States.Data;
using An01malia.FirstPerson.PlayerModule.States.DTOs;
using UnityEngine;

namespace An01malia.FirstPerson.PlayerModule.States
{
    public class PlayerRunState : PlayerBaseState
    {
        #region Fields

        [SerializeField] private float _speed = 20.0f;
        [SerializeField] private float _acceleration = 2.0f;
        [SerializeField] private float _gravityPull = 10.0f;

        #endregion

        #region Overriden Methods

        public override void EnterState(PlayerActionDTO dto)
        {
            StateData = new PlayerStateData(dto)
            {
                IsRunPressed = true
            };
        }

        public override PlayerActionDTO ExitState()
        {
            StateData.SetData(new MovementActionDTO(Controller.velocity));

            return StateData.GetData();
        }

        public override void UpdateState()
        {
            SetSpeed();
            HandleMovement();
            CheckSwitchState();
        }

        public override void CheckSwitchState()
        {
            if (!Controller.isGrounded)
            {
                SwitchState(StateMachine.Fall());
                return;
            }

            if (HasNoInput())
            {
                StateData.SetData(new RunActionDTO(false));
                SwitchState(StateMachine.Idle());
            }
        }

        public override void TriggerSwitchState(ActionType action, ActionDTO dto = null)
        {
            base.TriggerSwitchState(action, dto);

            switch (action)
            {
                case ActionType.Run:
                    SwitchState(StateMachine.Walk());
                    break;

                case ActionType.Jump:
                    SwitchState(StateMachine.Jump());
                    break;

                case ActionType.GrabLedge:
                    SwitchState(StateMachine.GrabLedge());
                    break;

                case ActionType.Push:
                    StateData.SetData(dto);
                    SwitchState(StateMachine.Push());
                    break;

                case ActionType.Climb:
                    StateData.SetData(dto);
                    SwitchState(StateMachine.Climb());
                    break;

                case ActionType.Carry:
                    StateData.SetData(dto);
                    SwitchState(this, StateMachine.Carry());
                    break;

                case ActionType.Inventory when dto is ItemActionDTO:
                    StateData.SetData(dto);
                    SwitchState(StateMachine.Inventory());
                    break;

                case ActionType.Inventory:
                    SwitchState(StateMachine.Inventory());
                    break;

                case ActionType.Interact:
                    (dto as InteractiveActionDTO).Interactive.StartInteraction();
                    break;

                default:
                    break;
            }
        }

        #endregion

        #region Private Methods

        private void HandleMovement()
        {
            Vector3 movementVector = HandleInput();

            movementVector = movementVector * StateData.Speed + _gravityPull * Vector3.down;

            Controller.Move(movementVector * Time.fixedDeltaTime);
        }

        private void SetSpeed()
        {
            StateData.Speed = Mathf.Lerp(StateData.Speed, _speed, Time.fixedDeltaTime * _acceleration);
        }

        private Vector3 HandleInput()
        {
            Vector3 movementVector = Player.Transform.forward * Input.MovementInputValues.y +
                                     Player.Transform.right * Input.MovementInputValues.x;

            return movementVector.normalized;
        }

        private bool HasNoInput() => Input.MovementInputValues.y == 0.0f && Input.MovementInputValues.x == 0.0f;

        #endregion
    }
}
using An01malia.FirstPerson.PlayerModule.States.Data;
using An01malia.FirstPerson.PlayerModule.States.DTOs;
using UnityEngine;

namespace An01malia.FirstPerson.PlayerModule.States
{
    public class PlayerWalkState : PlayerBaseState
    {
        #region Fields

        [SerializeField] private float _speed = 10.0f;
        [SerializeField] private float _acceleration = 0.0f;
        [SerializeField] private float _gravityPull = 10.0f;

        #endregion

        #region Overriden Methods

        public override void EnterState(PlayerActionDTO dto)
        {
            StateData = new PlayerStateData(dto)
            {
                IsRunPressed = false,
                Speed = _speed
            };
        }

        public override PlayerActionDTO ExitState()
        {
            StateData.SetData(new MovementActionDTO(Controller.velocity));

            return StateData.GetData();
        }

        public override void UpdateState()
        {
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
                SwitchState(StateMachine.Idle());
            }
        }

        public override bool TrySwitchState(ActionType action, ActionDTO dto = null)
        {
            if (base.TrySwitchState(action, dto)) return false;

            switch (action)
            {
                case ActionType.Run when (dto as RunActionDTO).IsRunPressed:
                    SwitchState(StateMachine.Run());
                    return true;

                case ActionType.Crouch:
                    SwitchState(StateMachine.Crouch());
                    return true;

                case ActionType.Jump:
                    SwitchState(StateMachine.Jump());
                    return true;

                case ActionType.GrabLedge:
                    SwitchState(StateMachine.GrabLedge());
                    return true;

                case ActionType.Push:
                    StateData.SetData(dto);
                    SwitchState(StateMachine.Push());
                    return true;

                case ActionType.Climb:
                    StateData.SetData(dto);
                    SwitchState(StateMachine.Climb());
                    return true;

                case ActionType.Carry:
                    StateData.SetData(dto);
                    SwitchState(this, StateMachine.Carry());
                    return true;

                case ActionType.Inspect:
                    StateData.SetData(dto);
                    SwitchState(StateMachine.Inspect());
                    return true;

                case ActionType.Inventory when dto is TransformActionDTO:
                    StateData.SetData(dto);
                    SwitchState(StateMachine.Inventory());
                    return true;

                case ActionType.Inventory:
                    SwitchState(StateMachine.Inventory());
                    return true;

                case ActionType.Interact when dto is InteractiveActionDTO interactiveDto:
                    interactiveDto.Interactive.StartInteraction();
                    return false;

                case ActionType.Interact when dto is ItemStandActionDTO itemStandDto:
                    if (!itemStandDto.ItemStand.Item) return false;

                    StateData.SetData(new TransformActionDTO(itemStandDto.ItemStand.Item));
                    SwitchState(this, StateMachine.Carry());
                    return true;

                case ActionType.Dialogue:
                    StateData.SetData(dto);
                    SwitchState(StateMachine.Dialogue());
                    return true;

                default:
                    return false;
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

        private Vector3 HandleInput()
        {
            Vector3 movementVector = Player.Transform.forward * PlayerInput.MovementInputValues.y +
                                     Player.Transform.right * PlayerInput.MovementInputValues.x;

            return movementVector.normalized;
        }

        private bool HasNoInput() => PlayerInput.MovementInputValues.y == 0.0f && PlayerInput.MovementInputValues.x == 0.0f;

        #endregion
    }
}
using An01malia.FirstPerson.PlayerModule.States.Data;
using An01malia.FirstPerson.PlayerModule.States.DTOs;
using UnityEngine;

namespace An01malia.FirstPerson.PlayerModule.States
{
    public class PlayerJumpState : PlayerBaseState
    {
        #region Fields

        [SerializeField] private AnimationCurve _jumpCurve;
        [SerializeField] private int _numberOfJumps = 1;
        [SerializeField] private float _jumpForce = 5.0f;
        [SerializeField] private float _airborneSpeed = 5.0f;
        [SerializeField] private float _deceleration = 2.0f;

        private JumpStateData _data;

        #endregion

        #region Overriden Methods

        public override void EnterState(PlayerActionDTO dto)
        {
            StateData = new JumpStateData(_numberOfJumps - 1, dto)
            {
                Speed = GetInitialSpeed(dto.Speed)
            };

            _data = StateData as JumpStateData;

            Controller.slopeLimit = 90.0f;
        }

        public override PlayerActionDTO ExitState()
        {
            Controller.slopeLimit = 45.0f;

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
            if (IsFalling())
            {
                SwapState(StateMachine.Fall());
                return;
            }

            if (HitTheCeiling())
            {
                SwapState(StateMachine.Fall());
            }

            if (!Controller.isGrounded) return;

            if (HasNoInput())
            {
                SwapState(StateMachine.Idle());
                return;
            }

            if (StateData.IsRunPressed)
            {
                SwapState(StateMachine.Run());
                return;
            }

            SwapState(StateMachine.Walk());
            return;
        }

        public override bool TrySwitchState(ActionType action, ActionDTO dto = null)
        {
            if (base.TrySwitchState(action, dto)) return false;

            switch (action)
            {
                case ActionType.Run:
                    StateData.SetData(dto);
                    return false;

                case ActionType.Jump when _data.JumpsRemaining > 0:
                    SwapState(StateMachine.Jump());
                    return true;

                case ActionType.GrabLedge:
                    SwapState(StateMachine.GrabLedge());
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
            movementVector = HandleJump(movementVector);

            Controller.Move(movementVector * Time.fixedDeltaTime);

            _data.TimeInAir += Time.fixedDeltaTime;
        }

        private Vector3 HandleInput()
        {
            Vector3 movementVector = Player.Transform.forward * PlayerInput.MovementInputValues.y +
                                     Player.Transform.right * PlayerInput.MovementInputValues.x;

            return movementVector.normalized;
        }

        private Vector3 HandleJump(Vector3 movementVector)
        {
            Vector3 jumpVector = _jumpCurve.Evaluate(_data.TimeInAir) * _jumpForce * Vector3.up;
            movementVector = jumpVector + movementVector * StateData.Speed;

            return movementVector;
        }

        private float GetInitialSpeed(float speed) => speed != 0.0f ? speed : _airborneSpeed;

        private bool IsFalling() => Controller.velocity.y <= 0.0f;

        private bool HitTheCeiling() => Controller.collisionFlags == CollisionFlags.Above;

        private bool HasNoInput() => PlayerInput.MovementInputValues.y == 0.0f && PlayerInput.MovementInputValues.x == 0.0f;

        #endregion
    }
}
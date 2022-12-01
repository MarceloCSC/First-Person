using An01malia.FirstPerson.Core.References;
using An01malia.FirstPerson.PlayerModule.States.Data;
using An01malia.FirstPerson.PlayerModule.States.DTOs;
using UnityEngine;

namespace An01malia.FirstPerson.PlayerModule.States
{
    public class PlayerFallState : PlayerBaseState
    {
        #region Fields

        [SerializeField] private AnimationCurve _gravityCurve;
        [SerializeField] private float _airborneSpeed = 2.0f;
        [SerializeField] private float _gravityPull = 18.0f;
        [SerializeField] private float _maxVelocity = 90.0f;
        [SerializeField] private float _coyoteTime = 0.2f;

        private FallStateData _data;

        #endregion

        #region Overriden Methods

        public override void EnterState(PlayerActionDTO dto)
        {
            StateData = new FallStateData(dto);

            _data = StateData as FallStateData;
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
            if (!Controller.isGrounded) return;

            if (HasNoInput())
            {
                SwitchState(StateMachine.Idle());
                return;
            }

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
                case ActionType.Run:
                    StateData.SetData(dto);
                    break;

                case ActionType.Jump when _data.JumpsRemaining > 0 && _data.CoyoteTimeCounter < _coyoteTime:
                    SwitchState(StateMachine.Jump());
                    break;

                case ActionType.GrabLedge:
                    SwitchState(StateMachine.GrabLedge());
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
            Vector3 gravityVector = HandleGravity();

            SetTimer();
            SetMomentum();

            movementVector = gravityVector + _data.Momentum + movementVector * _airborneSpeed;

            Controller.Move(movementVector * Time.fixedDeltaTime);
        }

        private Vector3 HandleInput()
        {
            Vector3 movementVector = Player.Transform.forward * Input.MovementInputValues.y +
                                     Player.Transform.right * Input.MovementInputValues.x;

            return movementVector.normalized;
        }

        private Vector3 HandleGravity()
        {
            Vector3 gravityVector = _gravityCurve.Evaluate(_data.TimeFalling) * _gravityPull * Vector3.down;

            gravityVector.y = Mathf.Clamp(gravityVector.y, -_maxVelocity, 0.0f);

            return gravityVector;
        }

        private void SetTimer()
        {
            _data.TimeFalling += Time.fixedDeltaTime;
            _data.CoyoteTimeCounter += Time.fixedDeltaTime;
        }

        private void SetMomentum()
        {
            _data.Momentum = Vector3.Lerp(_data.Momentum, Vector3.zero, Time.fixedDeltaTime * 5.0f);
        }

        private bool HasNoInput() => Input.MovementInputValues.y == 0.0f && Input.MovementInputValues.x == 0.0f;

        #endregion
    }
}
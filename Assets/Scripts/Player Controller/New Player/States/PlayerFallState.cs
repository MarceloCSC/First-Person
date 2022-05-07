using UnityEngine;

namespace FirstPerson.Player.States
{
    public class PlayerFallState : PlayerBaseState
    {
        #region Fields

        [SerializeField] private AnimationCurve _gravityCurve;
        [SerializeField] private float _airborneSpeed = 2.0f;
        [SerializeField] private float _gravityPull = 18.0f;
        [SerializeField] private float _maxVelocity = 90.0f;
        [SerializeField] private float _coyoteTime = 0.2f;

        private float _timeFalling;
        private float _coyoteTimeCounter;
        private Vector3 _momentum;
        private Vector3 _gravityVector;
        private Vector3 _movementVector;

        #endregion

        #region Overriden Methods

        public override void EnterState()
        {
            _timeFalling = 0.0f;
            _coyoteTimeCounter = 0.0f;
            _momentum = _context.Momentum;
        }

        public override void ExitState()
        {
            _context.Momentum = _momentum;
        }

        public override void UpdateState()
        {
            HandleGravity();
            CheckSwitchState();
        }

        public override void CheckSwitchState()
        {
            if (_characterController.isGrounded)
            {
                if (_inputManager.MovementInputValues.y != 0.0f || _inputManager.MovementInputValues.x != 0.0f)
                {
                    if (_context.IsRunPressed)
                    {
                        SwitchState(_stateMachine.Run());
                    }
                    else
                    {
                        SwitchState(_stateMachine.Walk());
                    }
                }
                else
                {
                    SwitchState(_stateMachine.Idle());
                }
            }
        }

        public override bool TrySwitchState(ActionType action)
        {
            TrySwitchSubState(action);

            if (action == ActionType.Jump && _context.JumpsRemaining > 0 && _coyoteTimeCounter < _coyoteTime)
            {
                SwitchState(_stateMachine.Jump());
                return true;
            }
            else if (action == ActionType.ClimbUpLedge)
            {
                SwitchState(_stateMachine.ClimbUpLedge());
                return true;
            }

            return false;
        }

        #endregion

        #region Private Methods

        private void HandleGravity()
        {
            _movementVector = transform.forward * _inputManager.MovementInputValues.y + transform.right * _inputManager.MovementInputValues.x;
            _movementVector.Normalize();

            _gravityVector = _gravityCurve.Evaluate(_timeFalling) * _gravityPull * Vector3.down;
            _gravityVector.y = Mathf.Clamp(_gravityVector.y, -_maxVelocity, 0.0f);

            _timeFalling += Time.fixedDeltaTime;
            _coyoteTimeCounter += Time.fixedDeltaTime;

            _momentum = Vector3.Lerp(_momentum, Vector3.zero, Time.fixedDeltaTime * 5.0f);

            _movementVector = _gravityVector + _momentum + _movementVector * _airborneSpeed;

            _characterController.Move(_movementVector * Time.fixedDeltaTime);
        }

        #endregion
    }
}
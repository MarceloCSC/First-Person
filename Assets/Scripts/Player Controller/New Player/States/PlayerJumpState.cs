using UnityEngine;

namespace FirstPerson.Player.States
{
    public class PlayerJumpState : PlayerBaseState
    {
        #region Fields

        [SerializeField] private AnimationCurve _jumpCurve;
        [SerializeField] private float _jumpForce = 5.0f;
        [SerializeField] private float _airborneSpeed = 5.0f;
        [SerializeField] private float _deceleration = 2.0f;

        private float _timeInAir;
        private float _movementSpeed;
        private Vector3 _jumpVector;
        private Vector3 _movementVector;

        #endregion

        #region Overriden Methods

        public override void EnterState()
        {
            _timeInAir = 0.0f;
            _movementSpeed = _context.MovementSpeed != 0.0f ? _context.MovementSpeed : _airborneSpeed;
            _characterController.slopeLimit = 90.0f;
            _context.JumpsRemaining--;
        }

        public override void ExitState()
        {
            _context.MovementSpeed = _movementSpeed;
            _context.Momentum = _characterController.velocity;
            _characterController.slopeLimit = 45.0f;
        }

        public override void UpdateState()
        {
            HandleJump();
            CheckSwitchState();
        }

        public override void CheckSwitchState()
        {
            if (_characterController.velocity.y <= 0.0f)
            {
                SwitchState(_stateMachine.Fall());
            }
            else if (_characterController.isGrounded)
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
            else if (_characterController.collisionFlags == CollisionFlags.Above)
            {
                SwitchState(_stateMachine.Fall());
            }
        }

        public override bool TrySwitchState(ActionType action)
        {
            TrySwitchSubState(action);

            if (action == ActionType.ClimbUpLedge)
            {
                SwitchState(_stateMachine.ClimbUpLedge());
                return true;
            }

            return false;
        }

        #endregion

        #region Private Methods

        private void HandleJump()
        {
            _movementVector = transform.forward * _inputManager.MovementInputValues.y + transform.right * _inputManager.MovementInputValues.x;
            _movementVector.Normalize();

            _jumpVector = _jumpCurve.Evaluate(_timeInAir) * _jumpForce * Vector3.up;
            _movementVector = _jumpVector + _movementVector * _movementSpeed;

            _characterController.Move(_movementVector * Time.fixedDeltaTime);

            _timeInAir += Time.fixedDeltaTime;
        }

        #endregion
    }
}
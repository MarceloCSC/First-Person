using FirstPerson.Interaction;
using UnityEngine;

namespace FirstPerson.Player.States
{
    public class PlayerWalkState : PlayerBaseState
    {
        #region Fields

        [SerializeField] private float _walkSpeed = 10.0f;
        [SerializeField] private float _acceleration = 0.0f;
        [SerializeField] private float _gravityPull = 10.0f;

        private Vector3 _movementVector;

        #endregion

        #region Overriden Methods

        public override void EnterState()
        {
        }

        public override void ExitState()
        {
            _context.MovementSpeed = _walkSpeed;
            _context.Momentum = _characterController.velocity;
        }

        public override void UpdateState()
        {
            HandleMovement();
            CheckSwitchState();
        }

        public override void CheckSwitchState()
        {
            if (!_characterController.isGrounded)
            {
                SwitchState(_stateMachine.Fall());
            }
            else if (_inputManager.MovementInputValues.y == 0.0f && _inputManager.MovementInputValues.x == 0.0f)
            {
                SwitchState(_stateMachine.Idle());
            }
        }

        public override bool TrySwitchState(ActionType action)
        {
            TrySwitchSubState(action);

            if (action == ActionType.Jump)
            {
                SwitchState(_stateMachine.Jump());
                return true;
            }
            else if (action == ActionType.ClimbUpLedge)
            {
                SwitchState(_stateMachine.ClimbUpLedge());
                return true;
            }
            else if (action == ActionType.Run && !_context.IsRunPressed)
            {
                SwitchState(_stateMachine.Run());
                return true;
            }
            else if (action == ActionType.Crouch)
            {
                SwitchState(_stateMachine.Crouch());
                return true;
            }
            else if (action == ActionType.Climb)
            {
                SwitchState(_stateMachine.Climb());
                return true;
            }
            else if (action == ActionType.Push)
            {
                SwitchState(_stateMachine.Push());
                return true;
            }
            else if (action == ActionType.PickUp)
            {
                SwitchSubState(_stateMachine.PickUp());
                return true;
            }
            else if (action == ActionType.Interact)
            {
                if (_context.InteractiveItem.TryGetComponent(out IInteractive interactive))
                {
                    interactive.StartInteraction();
                    return true;
                }
            }

            return false;
        }

        #endregion

        #region Private Methods

        private void HandleMovement()
        {
            _movementVector = transform.forward * _inputManager.MovementInputValues.y + transform.right * _inputManager.MovementInputValues.x;
            _movementVector.Normalize();

            _movementVector = _movementVector * _walkSpeed + _gravityPull * Vector3.down;

            _characterController.Move(_movementVector * Time.fixedDeltaTime);
        }

        #endregion
    }
}
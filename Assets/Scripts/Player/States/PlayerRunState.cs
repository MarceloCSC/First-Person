using An01malia.FirstPerson.Interaction;
using UnityEngine;

namespace An01malia.FirstPerson.Player.States
{
    public class PlayerRunState : PlayerBaseState
    {
        #region Fields

        [SerializeField] private float _runSpeed = 20.0f;
        [SerializeField] private float _acceleration = 2.0f;
        [SerializeField] private float _gravityPull = 10.0f;

        private float _movementSpeed;
        private Vector3 _movementVector;

        #endregion

        #region Overriden Methods

        public override void EnterState()
        {
            _movementSpeed = _context.MovementSpeed;
        }

        public override void ExitState()
        {
            _context.MovementSpeed = _movementSpeed;
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
            else if (action == ActionType.Run)
            {
                SwitchState(_stateMachine.Walk());
                return true;
            }
            else if (action == ActionType.ClimbUpLedge)
            {
                SwitchState(_stateMachine.ClimbUpLedge());
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

            _movementSpeed = Mathf.Lerp(_movementSpeed, _runSpeed, Time.fixedDeltaTime * _acceleration);
            _movementVector = _movementVector * _movementSpeed + _gravityPull * Vector3.down;

            _characterController.Move(_movementVector * Time.fixedDeltaTime);
        }

        #endregion
    }
}
using FirstPerson.Interaction;
using UnityEngine;

namespace FirstPerson.Player.States
{
    public class PlayerIdleState : PlayerBaseState
    {
        #region Fields

        [SerializeField] private float _gravityPull = 10.0f;

        private Vector3 _gravityVector;

        #endregion

        #region Overriden Methods

        public override void EnterState()
        {
            _context.MovementSpeed = 0.0f;
            _context.Momentum = Vector3.zero;
            _context.JumpsRemaining = 1;
        }

        public override void ExitState()
        {
        }

        public override void UpdateState()
        {
            HandleGravity();
            CheckSwitchState();
        }

        public override void CheckSwitchState()
        {
            if (!_characterController.isGrounded)
            {
                SwitchState(_stateMachine.Fall());
            }
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

        private void HandleGravity()
        {
            _gravityVector = _gravityPull * Vector3.down;

            _characterController.Move(_gravityVector * Time.fixedDeltaTime);
        }

        #endregion
    }
}
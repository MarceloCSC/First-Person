using An01malia.FirstPerson.Player.States;
using UnityEngine;

namespace An01malia.FirstPerson.Player
{
    public class PlayerStateMachine : MonoBehaviour
    {
        #region Fields

        private PlayerIdleState _idleState;
        private PlayerWalkState _walkState;
        private PlayerRunState _runState;
        private PlayerCrouchState _crouchState;
        private PlayerJumpState _jumpState;
        private PlayerFallState _fallState;
        private PlayerClimbState _climbState;
        private PlayerPushState _pushState;
        private PlayerPickUpState _pickUpState;
        private PlayerClimbUpLedgeState _climbUpLedgeState;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            TryGetComponent(out _idleState);
            TryGetComponent(out _walkState);
            TryGetComponent(out _runState);
            TryGetComponent(out _crouchState);
            TryGetComponent(out _jumpState);
            TryGetComponent(out _fallState);
            TryGetComponent(out _climbState);
            TryGetComponent(out _pushState);
            TryGetComponent(out _pickUpState);
            TryGetComponent(out _climbUpLedgeState);
        }

        #endregion

        #region Public Methods

        public PlayerBaseState Idle()
        {
            return _idleState;
        }

        public PlayerBaseState Walk()
        {
            return _walkState;
        }

        public PlayerBaseState Run()
        {
            return _runState;
        }

        public PlayerBaseState Crouch()
        {
            return _crouchState;
        }

        public PlayerBaseState Jump()
        {
            return _jumpState;
        }

        public PlayerBaseState Fall()
        {
            return _fallState;
        }

        public PlayerBaseState Climb()
        {
            return _climbState;
        }

        public PlayerBaseState Push()
        {
            return _pushState;
        }

        public PlayerBaseState PickUp()
        {
            return _pickUpState;
        }

        public PlayerBaseState ClimbUpLedge()
        {
            return _climbUpLedgeState;
        }

        #endregion
    }
}
using FirstPerson.Player.States;
using UnityEngine;

namespace FirstPerson.Player
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
            _idleState = GetComponent<PlayerIdleState>();
            _walkState = GetComponent<PlayerWalkState>();
            _runState = GetComponent<PlayerRunState>();
            _crouchState = GetComponent<PlayerCrouchState>();
            _jumpState = GetComponent<PlayerJumpState>();
            _fallState = GetComponent<PlayerFallState>();
            _climbState = GetComponent<PlayerClimbState>();
            _pushState = GetComponent<PlayerPushState>();
            _pickUpState = GetComponent<PlayerPickUpState>();
            _climbUpLedgeState = GetComponent<PlayerClimbUpLedgeState>();
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
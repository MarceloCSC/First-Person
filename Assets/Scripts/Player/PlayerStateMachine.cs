using An01malia.FirstPerson.PlayerModule.States;
using UnityEngine;

namespace An01malia.FirstPerson.PlayerModule
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
        private PlayerGrabLedgeState _grabLedgeState;
        private PlayerPushState _pushState;
        private PlayerClimbState _climbState;
        private PlayerCarryState _carryState;
        private PlayerInventoryState _inventoryState;
        private PlayerInspectState _inspectState;
        private PlayerDialogueState _dialogueState;

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
            TryGetComponent(out _grabLedgeState);
            TryGetComponent(out _pushState);
            TryGetComponent(out _climbState);
            TryGetComponent(out _carryState);
            TryGetComponent(out _inventoryState);
            TryGetComponent(out _inspectState);
            TryGetComponent(out _dialogueState);
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

        public PlayerBaseState GrabLedge()
        {
            return _grabLedgeState;
        }

        public PlayerBaseState Push()
        {
            return _pushState;
        }

        public PlayerBaseState Climb()
        {
            return _climbState;
        }

        public BaseState Carry()
        {
            return _carryState;
        }

        public PlayerBaseState Inventory()
        {
            return _inventoryState;
        }

        public PlayerBaseState Inspect()
        {
            return _inspectState;
        }

        public PlayerBaseState Dialogue()
        {
            return _dialogueState;
        }

        #endregion
    }
}
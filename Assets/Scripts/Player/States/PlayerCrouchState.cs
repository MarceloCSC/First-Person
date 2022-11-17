using System.Collections;
using An01malia.FirstPerson.Interaction;
using UnityEngine;

namespace An01malia.FirstPerson.Player.States
{
    public class PlayerCrouchState : PlayerBaseState
    {
        #region Fields

        [SerializeField] private float _walkSpeed = 5.0f;
        [SerializeField] private float _gravityPull = 10.0f;
        [SerializeField] private float _crouchingHeight = 0.6f;
        [SerializeField] private float _cameraHeight = 0.5f;
        [SerializeField] private float _distanceToCeiling = 1.0f;
        [SerializeField] private float _smoothTime = 0.1f;

        private bool _skipTransition;
        private float _standingHeight;
        private Vector3 _movementVector;
        private Coroutine _crouchCoroutine;

        #endregion

        #region Overriden Methods

        public override void EnterState()
        {
            _standingHeight = _characterController.height;
            _skipTransition = false;

            if (_crouchCoroutine != null) StopCoroutine(_crouchCoroutine);

            _crouchCoroutine = StartCoroutine(TranslateHeight(_crouchingHeight, _cameraHeight));
        }

        public override void ExitState()
        {
            _context.MovementSpeed = _walkSpeed;
            _context.Momentum = _characterController.velocity;

            if (_crouchCoroutine != null) StopCoroutine(_crouchCoroutine);

            if (_skipTransition)
            {
                _characterController.height = _standingHeight;
                _camera.CameraTransform.localPosition = _camera.CameraPosition;
            }
            else
            {
                _crouchCoroutine = StartCoroutine(TranslateHeight(_standingHeight, _camera.CameraPosition.y));
            }
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
                _skipTransition = true;
                SwitchState(_stateMachine.Fall());
            }
        }

        public override bool TrySwitchState(ActionType action)
        {
            TrySwitchSubState(action);

            if (action == ActionType.Jump && CanStandUp())
            {
                _skipTransition = true;
                SwitchState(_stateMachine.Jump());
                return true;
            }
            else if (action == ActionType.Run && CanStandUp())
            {
                SwitchState(_stateMachine.Run());
                return true;
            }
            else if (action == ActionType.Crouch && CanStandUp())
            {
                SwitchState(_stateMachine.Idle());
                return true;
            }
            else if (action == ActionType.Climb && CanStandUp())
            {
                SwitchState(_stateMachine.Climb());
                return true;
            }
            else if (action == ActionType.Push && CanStandUp())
            {
                SwitchState(_stateMachine.Push());
                return true;
            }
            else if (action == ActionType.ClimbUpLedge && CanStandUp())
            {
                SwitchState(_stateMachine.ClimbUpLedge());
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

        private IEnumerator TranslateHeight(float targetHeight, float targetYPosition)
        {
            float velocity = 0.0f;
            Vector3 velocityVector = Vector3.zero;
            Vector3 targetPosition = new Vector3(0.0f, targetYPosition, 0.0f);

            while (Mathf.Abs(_camera.CameraTransform.localPosition.y - targetYPosition) >= 0.01f &&
                    Mathf.Abs(_characterController.height - targetHeight) >= 0.01f)
            {
                _characterController.height = Mathf.SmoothDamp(_characterController.height, targetHeight, ref velocity, _smoothTime);
                _camera.CameraTransform.localPosition = Vector3.SmoothDamp(_camera.CameraTransform.localPosition, targetPosition, ref velocityVector, _smoothTime);

                yield return new WaitForFixedUpdate();
            }

            _characterController.height = targetHeight;
            _camera.CameraTransform.localPosition = targetPosition;
            _crouchCoroutine = null;

            yield return null;
        }

        private bool CanStandUp() => !Physics.Raycast(transform.position, transform.up, _distanceToCeiling);

        #endregion
    }
}
using System.Collections;
using An01malia.FirstPerson.Interaction;
using UnityEngine;

namespace An01malia.FirstPerson.Player.States
{
    public class PlayerClimbState : PlayerBaseState
    {
        #region Fields

        [SerializeField] private float _climbSpeed = 5.0f;
        [SerializeField] private float _approachSpeed = 10.0f;
        [SerializeField] private float _rayLength = 1.0f;
        [SerializeField] private LayerMask _layerToClimb;

        private bool _isClimbing;
        private bool _canClimbUpwards;
        private bool _canClimbSideways;
        private float _movementSpeed;
        private Vector3 _surfaceDirection;
        private Vector3 _surfaceRightAxis;
        private Vector3 _movementVector;

        #endregion

        #region Overriden Methods

        public override void EnterState()
        {
            _isClimbing = false;
            _movementSpeed = _context.MovementSpeed != 0.0f ? _context.MovementSpeed : _approachSpeed;

            StartCoroutine(ApproachToClimb());
        }

        public override void ExitState()
        {
        }

        public override void UpdateState()
        {
            if (_isClimbing)
            {
                HandleMovement();
                CheckSwitchState();
            }
        }

        public override void CheckSwitchState()
        {
            if (_characterController.isGrounded)
            {
                SwitchState(_stateMachine.Idle());
            }
            else if (!Physics.Raycast(transform.position + Vector3.down * _characterController.height / 2, _surfaceDirection, _rayLength, _layerToClimb))
            {
                SwitchState(_stateMachine.Fall());
            }
        }

        public override bool TrySwitchState(ActionType action)
        {
            TrySwitchSubState(action);

            if (action == ActionType.Climb)
            {
                SwitchState(_stateMachine.Fall());
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

        private void HandleMovement()
        {
            _movementVector = transform.up * _inputManager.MovementInputValues.y + _surfaceRightAxis * _inputManager.MovementInputValues.x;
            _movementVector.Normalize();

            if (!_canClimbUpwards)
            {
                _movementVector.y = 0.0f;
            }
            else if (!_canClimbSideways)
            {
                _movementVector.x = 0.0f;
                _movementVector.z = 0.0f;
            }

            _characterController.Move(_movementVector * _climbSpeed * Time.fixedDeltaTime);
        }

        private IEnumerator ApproachToClimb()
        {
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, Vector3.Distance(transform.position, _context.InteractiveItem.position), _layerToClimb))
            {
                Vector3 targetPosition = hit.point + hit.normal * transform.localScale.z / 2;
                targetPosition.y = transform.position.y;

                _surfaceDirection = Vector3.Normalize(hit.point - transform.position);
                _surfaceRightAxis = Vector3.Normalize(Vector3.Cross(hit.normal, Vector3.up));

                if (hit.transform.TryGetComponent(out Climbable climbable))
                {
                    _canClimbUpwards = climbable.CanClimbUpwards;
                    _canClimbSideways = climbable.CanClimbSideways;
                }

                while (Vector3.Distance(transform.position, targetPosition) >= 0.1f)
                {
                    transform.position = Vector3.MoveTowards(transform.position, targetPosition, _movementSpeed * Time.fixedDeltaTime);

                    yield return new WaitForFixedUpdate();
                }

                _isClimbing = true;
            }

            yield return null;
        }

        #endregion
    }
}
using System.Collections;
using An01malia.FirstPerson.Interaction;
using UnityEngine;

namespace An01malia.FirstPerson.Player.States
{
    public class PlayerPushState : PlayerBaseState
    {
        #region Fields

        [SerializeField] private float _approachSpeed = 10.0f;
        [SerializeField] private float _gravityPull = 10.0f;
        [SerializeField] private float _distanceToBox = 0.2f;
        [SerializeField] private float _distanceToCollision = 0.1f;
        [SerializeField] private LayerMask _layerToPush;
        [SerializeField] private LayerMask _layersToCollide;

        private bool _isPushing;
        private bool _isZAxisAligned;
        private bool _canPushForward;
        private bool _canPushSideways;
        private float _currentSpeed;
        private float _movementSpeed;
        private float _acceleration;
        private Vector3 _movementVector;
        private Vector3 _currentInputVector;
        private Vector3 _inputVector;
        private Vector3 _boxPosition;
        private Vector3 _differenceToPlayer;
        private Vector3 _facingDirection;
        private Vector3 _colliderSize;
        private Transform _boxTransform;

        #endregion

        #region Overriden Methods

        public override void EnterState()
        {
            _boxTransform = _context.InteractiveItem;
            _colliderSize = _boxTransform.GetComponent<BoxCollider>().size;
            _currentSpeed = 0.0f;
            _movementSpeed = _context.MovementSpeed != 0.0f ? _context.MovementSpeed : _approachSpeed;

            StartCoroutine(ApproachToPush());
        }

        public override void ExitState()
        {
            _isPushing = false;
            _boxTransform = null;
        }

        public override void UpdateState()
        {
            if (_isPushing)
            {
                HandleInput();
                HandleMovement();
                CheckCollision();
                CheckSwitchState();
            }
        }

        public override void CheckSwitchState()
        {
            if (!_characterController.isGrounded)
            {
                SwitchState(_stateMachine.Fall());
            }
        }

        public override bool TrySwitchState(ActionType action)
        {
            TrySwitchSubState(action);

            if (action == ActionType.Push)
            {
                if (_context.InteractiveItem.gameObject == _boxTransform.gameObject)
                {
                    SwitchState(_stateMachine.Idle());
                    return true;
                }
            }
            else if (action == ActionType.None)
            {
                // Allow player to stop pushing when not looking at the object
                SwitchState(_stateMachine.Idle());
                return true;
            }

            return false;
        }

        #endregion

        #region Private Methods

        private void HandleInput()
        {
            if (_inputManager.MovementInputValues.y != 0.0f && (_isZAxisAligned && _canPushForward || !_isZAxisAligned && _canPushSideways))
            {
                _currentInputVector = _facingDirection * _inputManager.MovementInputValues.y;
                _currentInputVector.Normalize();
            }
            else if (_inputManager.MovementInputValues.x != 0.0f && (_isZAxisAligned && _canPushSideways || !_isZAxisAligned && _canPushForward))
            {
                _currentInputVector = Vector3.Cross(Vector3.up, _facingDirection) * _inputManager.MovementInputValues.x;
                _currentInputVector.Normalize();
            }
            else
            {
                _currentInputVector = Vector3.zero;
            }
        }

        private void HandleMovement()
        {
            _currentSpeed = SetSpeed();
            _inputVector = _currentInputVector;

            _movementVector = _inputVector * _currentSpeed + _gravityPull * Vector3.down;

            _characterController.Move(_movementVector * Time.fixedDeltaTime);

            _boxPosition = transform.position + _differenceToPlayer;
            _boxPosition.y = _boxTransform.position.y;
            _boxTransform.position = _boxPosition;
        }

        private void CheckCollision()
        {
            if (Physics.BoxCast(_boxTransform.localPosition, _colliderSize / 2, _inputVector, out RaycastHit hit, _boxTransform.localRotation, _distanceToCollision, _layersToCollide))
            {
                if ((_inputVector.x != 0 && Mathf.Sign(_inputVector.x) != Mathf.Sign(hit.normal.x)) ||
                    (_inputVector.z != 0 && Mathf.Sign(_inputVector.z) != Mathf.Sign(hit.normal.z)))
                {
                    _inputVector = Vector3.zero;
                }
            }
        }

        private float SetSpeed()
        {
            if (_currentInputVector != _inputVector || _currentInputVector == Vector3.zero)
            {
                return 0.0f;
            }
            else
            {
                return Mathf.Lerp(_currentSpeed, _movementSpeed, Time.fixedDeltaTime * _acceleration);
            }
        }

        private IEnumerator ApproachToPush()
        {
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, Vector3.Distance(transform.position, _boxTransform.position), _layerToPush))
            {
                _facingDirection = -hit.normal;
                _facingDirection.y = 0.0f;

                Vector3 targetPosition = hit.collider.ClosestPoint(transform.position) + hit.normal * (transform.localScale.z / 2 + _distanceToBox);
                targetPosition.y = transform.position.y;

                while (Vector3.Distance(transform.position, targetPosition) >= 0.1f)
                {
                    transform.position = Vector3.MoveTowards(transform.position, targetPosition, _movementSpeed * Time.fixedDeltaTime);

                    yield return new WaitForFixedUpdate();
                }

                _differenceToPlayer = _boxTransform.position - transform.position;

                if (_boxTransform.TryGetComponent(out Pushable pushableBox))
                {
                    _isZAxisAligned = _boxTransform.forward == _facingDirection || _boxTransform.forward == -_facingDirection;
                    _canPushForward = pushableBox.CanMoveForward;
                    _canPushSideways = pushableBox.CanMoveSideways;
                    _movementSpeed = pushableBox.MovementSpeed;
                    _acceleration = pushableBox.Acceleration;
                }

                _isPushing = true;
            }

            yield return null;
        }

        #endregion
    }
}
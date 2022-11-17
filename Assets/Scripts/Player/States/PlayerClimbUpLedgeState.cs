using System.Collections;
using UnityEngine;

namespace An01malia.FirstPerson.Player.States
{
    public class PlayerClimbUpLedgeState : PlayerBaseState
    {
        #region Fields

        [SerializeField] private float _speed = 10.0f;
        [SerializeField] private float _rayLength = 0.75f;
        [SerializeField] private Vector3 _lowerBounds = new Vector3(0.0f, 0.9f, 0.0f);

        private bool _isClimbingUp;
        private float _movementSpeed;

        #endregion

        #region Overriden Methods

        public override void EnterState()
        {
            _isClimbingUp = true;
            _movementSpeed = _context.MovementSpeed != 0.0f ? _context.MovementSpeed : _speed;

            StartCoroutine(ClimbUpLedge());
        }

        public override void ExitState()
        {
        }

        public override void UpdateState()
        {
            CheckSwitchState();
        }

        public override void CheckSwitchState()
        {
            if (!_isClimbingUp)
            {
                SwitchState(_stateMachine.Idle());
            }
        }

        public override bool TrySwitchState(ActionType action)
        {
            TrySwitchSubState(action);

            return false;
        }

        #endregion

        #region Private Methods

        private IEnumerator ClimbUpLedge()
        {
            if (Physics.Raycast(transform.position - _lowerBounds, transform.forward, out RaycastHit hit, _rayLength))
            {
                Vector3 topPosition = hit.collider.bounds.center + new Vector3(0.0f, hit.collider.bounds.extents.y);
                Vector3 targetPosition = new Vector3(transform.position.x, _characterController.bounds.extents.y + topPosition.y, transform.position.z);

                while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
                {
                    transform.position = Vector3.MoveTowards(transform.position, targetPosition, _movementSpeed * Time.fixedDeltaTime);

                    yield return new WaitForFixedUpdate();
                }

                targetPosition += transform.forward * transform.localScale.z;

                while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
                {
                    transform.position = Vector3.MoveTowards(transform.position, targetPosition, _movementSpeed * Time.fixedDeltaTime);

                    yield return new WaitForFixedUpdate();
                }

                _isClimbingUp = false;
            }

            yield return null;
        }

        #endregion
    }
}
using An01malia.FirstPerson.InteractionModule.Environment;
using An01malia.FirstPerson.PlayerModule.States.Data;
using An01malia.FirstPerson.PlayerModule.States.DTOs;
using System.Collections;
using UnityEngine;

namespace An01malia.FirstPerson.PlayerModule.States
{
    public class PlayerClimbState : PlayerBaseState
    {
        #region Fields

        [SerializeField] private float _speed = 5.0f;
        [SerializeField] private float _speedToApproach = 10.0f;
        [SerializeField] private float _rayLength = 1.0f;
        [SerializeField] private LayerMask _layerToClimb;

        private ClimbStateData _data;

        #endregion

        #region Overriden Methods

        public override void EnterState(PlayerActionDTO dto)
        {
            StateData = new ClimbStateData(dto.Transform.position, dto)
            {
                Speed = GetInitialSpeed(dto.Speed),
            };

            _data = StateData as ClimbStateData;
            _data.Coroutine = StartCoroutine(ApproachToClimb());
        }

        public override PlayerActionDTO ExitState()
        {
            if (_data.Coroutine != null) StopCoroutine(_data.Coroutine);

            StateData.SetData(new MovementActionDTO(Controller.velocity));

            return StateData.GetData();
        }

        public override void UpdateState()
        {
            if (!_data.IsClimbing) return;

            HandleMovement();
            CheckSwitchState();
        }

        public override void CheckSwitchState()
        {
            if (Controller.isGrounded)
            {
                SwapState(StateMachine.Idle());
                return;
            }

            if (HasNoSurfaceToClimb())
            {
                SwapState(StateMachine.Fall());
            }
        }

        public override bool TrySwitchState(ActionType action, ActionDTO dto = null)
        {
            if (base.TrySwitchState(action, dto)) return false;

            switch (action)
            {
                case ActionType.Run:
                    StateData.SetData(dto);
                    return false;

                case ActionType.GrabLedge:
                    SwapState(StateMachine.GrabLedge());
                    return true;

                case ActionType.Climb:
                    SwapState(StateMachine.Fall());
                    return true;

                default:
                    return false;
            }
        }

        #endregion

        #region Private Methods

        private void HandleMovement()
        {
            Vector3 movementVector = HandleInput();
            movementVector = RestrainMovement(movementVector);

            Controller.Move(_speed * Time.fixedDeltaTime * movementVector);
        }

        private Vector3 HandleInput()
        {
            Vector3 movementVector = Player.Transform.up * PlayerInput.MovementInputValues.y +
                                     _data.SurfaceRightAxis * PlayerInput.MovementInputValues.x;

            return movementVector.normalized;
        }

        private Vector3 RestrainMovement(Vector3 movementVector)
        {
            if (!_data.CanClimbUpwards)
            {
                movementVector.y = 0.0f;
            }
            else if (!_data.CanClimbSideways)
            {
                movementVector.x = 0.0f;
                movementVector.z = 0.0f;
            }

            return movementVector;
        }

        private float GetInitialSpeed(float speed) => speed != 0.0f ? speed : _speedToApproach;

        private bool HasNoSurfaceToClimb() => !Physics.Raycast(Player.Transform.position + (Vector3.down * Controller.height / 2),
                                                               _data.SurfaceDirection,
                                                               _rayLength,
                                                               _layerToClimb);

        #region Coroutine

        private IEnumerator ApproachToClimb()
        {
            if (IsRaycastHitting(out RaycastHit hit))
            {
                Vector3 targetPosition = GetTargetPosition(hit);

                SetInitialData(hit);

                while (Vector3.Distance(Player.Transform.position, targetPosition) >= 0.1f)
                {
                    MovePlayerTowards(targetPosition);

                    yield return new WaitForFixedUpdate();
                }

                _data.IsClimbing = true;
            }

            _data.Coroutine = null;

            yield return null;
        }

        private void MovePlayerTowards(Vector3 targetPosition)
        {
            Player.Transform.position = Vector3.MoveTowards(Player.Transform.position,
                                                            targetPosition,
                                                            _data.Speed * Time.fixedDeltaTime);
        }

        private static Vector3 GetTargetPosition(RaycastHit hit)
        {
            Vector3 targetPosition = hit.point + hit.normal * Player.Transform.localScale.z / 2;
            targetPosition.y = Player.Transform.position.y;

            return targetPosition;
        }

        private bool IsRaycastHitting(out RaycastHit hit)
        {
            return Physics.Raycast(Player.Transform.position, Player.Transform.forward, out hit,
                                   Vector3.Distance(Player.Transform.position, _data.SurfacePosition), _layerToClimb);
        }

        private void SetInitialData(RaycastHit hit)
        {
            _data.SurfaceDirection = Vector3.Normalize(hit.point - Player.Transform.position);
            _data.SurfaceRightAxis = Vector3.Normalize(Vector3.Cross(hit.normal, Vector3.up));

            if (hit.transform.TryGetComponent(out SurfaceToClimb surface))
            {
                _data.CanClimbUpwards = surface.CanClimbUpwards;
                _data.CanClimbSideways = surface.CanClimbSideways;
            }
        }

        #endregion

        #endregion
    }
}
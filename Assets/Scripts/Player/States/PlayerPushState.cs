using An01malia.FirstPerson.Core.References;
using An01malia.FirstPerson.InteractionModule;
using An01malia.FirstPerson.PlayerModule.States.Data;
using An01malia.FirstPerson.PlayerModule.States.DTOs;
using System.Collections;
using UnityEngine;

namespace An01malia.FirstPerson.PlayerModule.States
{
    public class PlayerPushState : PlayerBaseState
    {
        #region Fields

        [SerializeField] private float _speedToApproach = 10.0f;
        [SerializeField] private float _gravityPull = 10.0f;
        [SerializeField] private float _distanceToPlayer = 0.2f;
        [SerializeField] private float _distanceToCollision = 0.1f;
        [SerializeField] private LayerMask _layerToPush;
        [SerializeField] private LayerMask _layersToCollide;

        private PushStateData _data;

        #endregion

        #region Overriden Methods

        public override void EnterState(PlayerActionDTO dto)
        {
            StateData = new PushStateData(dto.Item.GetComponent<BoxCollider>().size, dto)
            {
                Speed = GetInitialSpeed(dto.Speed),
            };

            _data = StateData as PushStateData;
            _data.Coroutine = StartCoroutine(ApproachToPush());
        }

        public override PlayerActionDTO ExitState()
        {
            if (_data.Coroutine != null) StopCoroutine(_data.Coroutine);

            StateData.SetData(new MovementActionDTO(Controller.velocity));

            return StateData.GetData();
        }

        public override void UpdateState()
        {
            if (!_data.IsPushing) return;

            Vector3 inputVector = GetInput();
            inputVector = HandleCollision(inputVector);

            _data.CurrentSpeed = GetSpeed(inputVector);

            HandleMovement(_data.CurrentSpeed, inputVector);

            _data.PreviousInputVector = inputVector;

            CheckSwitchState();
        }

        public override void CheckSwitchState()
        {
            if (Controller.isGrounded) return;

            SwitchState(StateMachine.Fall());
        }

        public override void TriggerSwitchState(ActionType action, ActionDTO dto = null)
        {
            base.TriggerSwitchState(action, dto);

            switch (action)
            {
                case ActionType.None:
                    SwitchState(StateMachine.Idle());
                    break;

                case ActionType.Run:
                    StateData.SetData(dto);
                    break;

                case ActionType.Push when (dto as ItemActionDTO).Item == _data.Item:
                    SwitchState(StateMachine.Idle());
                    break;

                default:
                    break;
            }
        }

        #endregion

        #region Private Methods

        private Vector3 GetInput()
        {
            Vector3 inputVector = Vector3.zero;

            if (IsPushingSideways())
            {
                inputVector = _data.FacingDirection * Input.MovementInputValues.y;
                inputVector.Normalize();
            }

            if (IsPushingForward())
            {
                inputVector = Vector3.Cross(Vector3.up, _data.FacingDirection) * Input.MovementInputValues.x;
                inputVector.Normalize();
            }

            return inputVector;
        }

        private void HandleMovement(float speed, Vector3 inputVector)
        {
            Vector3 movementVector = inputVector * speed + _gravityPull * Vector3.down;

            Controller.Move(movementVector * Time.fixedDeltaTime);

            _data.Item.position = GetItemPosition();
        }

        private Vector3 HandleCollision(Vector3 inputVector)
        {
            if (IsColliding(inputVector, out RaycastHit hit) && IsPushingAgainstObstacle(inputVector, hit))
            {
                return Vector3.zero;
            }

            return inputVector;
        }

        private float GetSpeed(Vector3 inputVector)
        {
            if (inputVector != _data.PreviousInputVector || inputVector == Vector3.zero) return 0.0f;

            return Mathf.Lerp(_data.CurrentSpeed, _data.Speed, Time.fixedDeltaTime * _data.Acceleration);
        }

        private float GetInitialSpeed(float speed) => speed != 0.0f ? speed : _speedToApproach;

        private bool IsPushingForward()
        {
            return Input.MovementInputValues.x != 0.0f && ((_data.IsZAxisAligned && _data.CanPushSideways) ||
                                                           (!_data.IsZAxisAligned && _data.CanPushForward));
        }

        private bool IsPushingSideways()
        {
            return Input.MovementInputValues.y != 0.0f && ((_data.IsZAxisAligned && _data.CanPushForward) ||
                                                           (!_data.IsZAxisAligned && _data.CanPushSideways));
        }

        private bool IsColliding(Vector3 inputVector, out RaycastHit hit)
        {
            return Physics.BoxCast(_data.Item.localPosition, _data.ColliderSize / 2, inputVector, out hit,
                                   _data.Item.localRotation, _distanceToCollision, _layersToCollide);
        }

        private static bool IsPushingAgainstObstacle(Vector3 inputVector, RaycastHit hit)
        {
            return (inputVector.x != 0 && Mathf.Sign(inputVector.x) != Mathf.Sign(hit.normal.x)) ||
                   (inputVector.z != 0 && Mathf.Sign(inputVector.z) != Mathf.Sign(hit.normal.z));
        }

        private Vector3 GetItemPosition()
        {
            Vector3 position = Player.Transform.position + _data.DifferenceToPlayer;
            position.y = _data.Item.position.y;

            return position;
        }

        #region Coroutine

        private IEnumerator ApproachToPush()
        {
            if (IsRaycastHitting(out RaycastHit hit))
            {
                _data.FacingDirection = new Vector3(-hit.normal.x, 0.0f, -hit.normal.z);

                Vector3 targetPosition = GetTargetPosition(hit);

                while (Vector3.Distance(Player.Transform.position, targetPosition) >= 0.1f)
                {
                    MovePlayerTowards(targetPosition);

                    yield return new WaitForFixedUpdate();
                }

                SetInitialData();

                _data.IsPushing = true;
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

        private Vector3 GetTargetPosition(RaycastHit hit)
        {
            Vector3 targetPosition = hit.collider.ClosestPoint(Player.Transform.position) +
                                     hit.normal * (Player.Transform.localScale.z / 2 + _distanceToPlayer);

            targetPosition.y = Player.Transform.position.y;

            return targetPosition;
        }

        private bool IsRaycastHitting(out RaycastHit hit)
        {
            return Physics.Raycast(Player.Transform.position, Player.Transform.forward, out hit,
                                   Vector3.Distance(Player.Transform.position, _data.Item.position), _layerToPush);
        }

        private void SetInitialData()
        {
            _data.DifferenceToPlayer = _data.Item.position - Player.Transform.position;

            if (_data.Item.TryGetComponent(out Pushable pushable))
            {
                _data.Acceleration = pushable.Acceleration;
                _data.Speed = pushable.MovementSpeed;
                _data.CanPushForward = pushable.CanMoveForward;
                _data.CanPushSideways = pushable.CanMoveSideways;
                _data.IsZAxisAligned = _data.Item.forward == _data.FacingDirection ||
                                       _data.Item.forward == -_data.FacingDirection;
            }
        }

        #endregion

        #endregion
    }
}
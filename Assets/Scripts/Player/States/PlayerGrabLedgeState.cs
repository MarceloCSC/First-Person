using An01malia.FirstPerson.PlayerModule.States.Data;
using An01malia.FirstPerson.PlayerModule.States.DTOs;
using System.Collections;
using UnityEngine;

namespace An01malia.FirstPerson.PlayerModule.States
{
    public class PlayerGrabLedgeState : PlayerBaseState
    {
        #region Fields

        [SerializeField] private float _speed = 10.0f;
        [SerializeField] private float _rayLength = 0.75f;
        [SerializeField] private Vector3 _lowerBounds = new(0.0f, 0.9f, 0.0f);

        private GrabLedgeStateData _data;

        #endregion

        #region Overriden Methods

        public override void EnterState(PlayerActionDTO dto)
        {
            StateData = new GrabLedgeStateData(dto)
            {
                Speed = GetInitialSpeed(dto.Speed)
            };

            _data = StateData as GrabLedgeStateData;

            StartCoroutine(GrabLedge());
        }

        public override PlayerActionDTO ExitState()
        {
            StateData.SetData(new MovementActionDTO(Controller.velocity));

            return StateData.GetData();
        }

        public override void UpdateState()
        {
            CheckSwitchState();
        }

        public override void CheckSwitchState()
        {
            if (_data.IsGrabbing) return;

            SwitchState(StateMachine.Idle());
        }

        public override bool TrySwitchState(ActionType action, ActionDTO dto = null)
        {
            if (base.TrySwitchState(action, dto)) return false;

            switch (action)
            {
                case ActionType.Run:
                    StateData.SetData(dto);
                    return false;

                default:
                    return false;
            }
        }

        #endregion

        #region Private Methods

        private float GetInitialSpeed(float speed) => speed != 0.0f ? speed : _speed;

        #region Coroutine

        private IEnumerator GrabLedge()
        {
            if (IsRaycastHitting(out RaycastHit hit))
            {
                Vector3 topPosition = GetTopPosition(hit);
                Vector3 targetPosition = GetTargetPosition(topPosition);

                while (Vector3.Distance(Player.Transform.position, targetPosition) > 0.01f)
                {
                    MovePlayerTowards(targetPosition);

                    yield return new WaitForFixedUpdate();
                }

                targetPosition += Player.Transform.forward * Player.Transform.localScale.z;

                while (Vector3.Distance(Player.Transform.position, targetPosition) > 0.01f)
                {
                    MovePlayerTowards(targetPosition);

                    yield return new WaitForFixedUpdate();
                }

                _data.IsGrabbing = false;
            }

            yield return null;
        }

        private static Vector3 GetTopPosition(RaycastHit hit)
        {
            return hit.collider.bounds.center + new Vector3(0.0f, hit.collider.bounds.extents.y);
        }

        private Vector3 GetTargetPosition(Vector3 topPosition)
        {
            return new(Player.Transform.position.x, Controller.bounds.extents.y + topPosition.y, Player.Transform.position.z);
        }

        private void MovePlayerTowards(Vector3 targetPosition)
        {
            Player.Transform.position = Vector3.MoveTowards(Player.Transform.position,
                                                            targetPosition,
                                                            StateData.Speed * Time.fixedDeltaTime);
        }

        private bool IsRaycastHitting(out RaycastHit hit)
        {
            return Physics.Raycast(Player.Transform.position - _lowerBounds, Player.Transform.forward, out hit, _rayLength);
        }

        #endregion

        #endregion
    }
}
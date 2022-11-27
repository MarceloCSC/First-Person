using An01malia.FirstPerson.Core.References;
using An01malia.FirstPerson.PlayerModule.States.Data;
using An01malia.FirstPerson.PlayerModule.States.DTOs;
using System.Collections;
using UnityEngine;

namespace An01malia.FirstPerson.PlayerModule.States
{
    public class PlayerCrouchState : PlayerBaseState
    {
        #region Fields

        [SerializeField] private float _walkSpeed = 5.0f;
        [SerializeField] private float _gravityPull = 10.0f;
        [SerializeField] private float _crouchingHeight = 1.0f;
        [SerializeField] private float _distanceToCeiling = 1.0f;
        [SerializeField] private float _interpolationRatio = 0.2f;
        [SerializeField] private float _smoothTime = 0.12f;

        private CrouchStateData _data;

        #endregion

        #region Overriden Methods

        public override void EnterState(PlayerActionDTO dto)
        {
            StateData = new CrouchStateData(Controller.height, Player.SightPosition, dto)
            {
                Speed = _walkSpeed,
            };

            _data = StateData as CrouchStateData;
            _data.Coroutine = StartCoroutine(TranslateHeight(_crouchingHeight,
                                                             GetTargetCenter(),
                                                             new Vector3(0.0f, _crouchingHeight)));
        }

        public override PlayerActionDTO ExitState()
        {
            StateData.SetData(new MovementActionDTO(Controller.velocity));

            return StateData.GetData();
        }

        public override void UpdateState()
        {
            HandleMovement();
            CheckSwitchState();
        }

        public override void CheckSwitchState()
        {
            if (Controller.isGrounded) return;

            StandUp(StateMachine.Fall(), true);
        }

        public override void TriggerSwitchState(ActionType action, ActionDTO dto = null)
        {
            base.TriggerSwitchState(action, dto);

            switch (action)
            {
                case ActionType.Jump when CanStandUp():
                    StandUp(StateMachine.Idle());
                    break;

                case ActionType.Run when CanStandUp() && (dto as RunActionDTO).IsRunPressed:
                    StandUp(StateMachine.Run());
                    break;

                case ActionType.Run:
                    StateData.SetData(dto);
                    break;

                case ActionType.Crouch when CanStandUp():
                    StandUp(StateMachine.Idle());
                    break;

                case ActionType.GrabLedge when CanStandUp():
                    StandUp(StateMachine.GrabLedge());
                    break;

                case ActionType.Climb when CanStandUp():
                    StateData.SetData(dto);
                    StandUp(StateMachine.Climb());
                    break;

                case ActionType.Push when CanStandUp():
                    StateData.SetData(dto);
                    StandUp(StateMachine.Push());
                    break;

                case ActionType.Carry:
                    StateData.SetData(dto);
                    SwitchState(this, StateMachine.Carry());
                    break;

                case ActionType.Interact:
                    (dto as InteractiveActionDTO).Interactive.StartInteraction();
                    break;

                default:
                    break;
            }
        }

        #endregion

        #region Private Methods

        private void HandleMovement()
        {
            Vector3 movementVector = HandleInput();

            movementVector = movementVector * _walkSpeed + _gravityPull * Vector3.down;

            Controller.Move(movementVector * Time.fixedDeltaTime);
        }

        private Vector3 HandleInput()
        {
            Vector3 movementVector = Player.Transform.forward * Input.MovementInputValues.y +
                                     Player.Transform.right * Input.MovementInputValues.x;

            return movementVector.normalized;
        }

        private bool CanStandUp() => !Physics.Raycast(Player.Transform.position, Player.Transform.up, _distanceToCeiling);

        private void StandUp(PlayerBaseState newState, bool skipTransition = false)
        {
            if (_data.Coroutine != null) StopCoroutine(_data.Coroutine);

            if (skipTransition)
            {
                SetFinalPosition(_data.StandingHeight, Vector3.zero, _data.InitialPosition);
                SwitchState(newState);

                return;
            }

            _data.Coroutine = StartCoroutine(TranslateHeight(_data.StandingHeight,
                                                             Vector3.zero,
                                                             _data.InitialPosition,
                                                             newState));
        }

        private Vector3 GetTargetCenter() => -new Vector3(0.0f, 1 - _crouchingHeight / Controller.height, 0.0f);

        private void SetFinalPosition(float targetHeight, Vector3 targetCenter, Vector3 targetPosition)
        {
            Controller.height = targetHeight;
            Controller.center = targetCenter;
            Player.Sight.localPosition = targetPosition;
        }

        #region Coroutine

        private IEnumerator TranslateHeight(float targetHeight, Vector3 targetCenter, Vector3 targetPosition)
        {
            Vector3 velocityVector = Vector3.zero;

            while (IsFarFromTarget(targetHeight, targetPosition.y))
            {
                Controller.height = Mathf.LerpUnclamped(Controller.height, targetHeight, _interpolationRatio);
                Controller.center = Vector3.LerpUnclamped(Controller.center, targetCenter, _interpolationRatio);

                velocityVector = MoveCameraTowards(targetPosition, velocityVector);

                yield return new WaitForFixedUpdate();
            }

            SetFinalPosition(targetHeight, targetCenter, targetPosition);

            _data.Coroutine = null;

            yield return null;
        }

        private IEnumerator TranslateHeight(float targetHeight,
                                            Vector3 targetCenter,
                                            Vector3 targetPosition,
                                            PlayerBaseState newState)
        {
            yield return TranslateHeight(targetHeight, targetCenter, targetPosition);

            SwitchState(newState);
        }

        private bool IsFarFromTarget(float targetHeight, float targetYPosition)
        {
            return Mathf.Abs(Player.Sight.position.y - targetYPosition) >= 0.01f &&
                   Mathf.Abs(Controller.height - targetHeight) >= 0.01f;
        }

        private Vector3 MoveCameraTowards(Vector3 targetPosition, Vector3 velocityVector)
        {
            Player.Sight.localPosition = Vector3.SmoothDamp(Player.Sight.localPosition,
                                                            targetPosition,
                                                            ref velocityVector,
                                                            _smoothTime);

            return velocityVector;
        }

        #endregion

        #endregion
    }
}
using An01malia.FirstPerson.Core;
using An01malia.FirstPerson.Core.References;
using UnityEngine;

namespace An01malia.FirstPerson.PlayerModule.States
{
    public abstract class PlayerBaseState : BaseState
    {
        #region Fields

        private readonly float _maxVerticalAngle = 90.0f;
        private readonly float _minVerticalAngle = -75.0f;
        private float _mouseX;
        private float _mouseY;
        private float _xAxisRotation;

        protected CharacterController Controller;
        protected PlayerStateMachine StateMachine;
        protected PlayerInput Input;

        private PlayerController _context;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            SetReferences();
        }

        #endregion

        #region Public Methods

        public virtual void UpdateCamera()
        {
            _mouseX = Input.ViewInputValues.x * GameOptions.MouseSensitivity * Time.deltaTime;
            _mouseY = Input.ViewInputValues.y * GameOptions.MouseSensitivity * Time.deltaTime;

            Player.CameraTransform.Rotate(Vector3.left * _mouseY);
            Player.Transform.Rotate(Vector3.up * _mouseX);

            ClampViewAngle();
        }

        #endregion

        #region Protected Methods

        protected override void SwitchState(BaseState newState)
        {
            if (newState == _context.CurrentState) return;

            base.SwitchState(newState);

            _context.SetCurrentState(newState as PlayerBaseState);
        }

        protected virtual void ClampViewAngle()
        {
            _xAxisRotation += _mouseY;

            if (_xAxisRotation > _maxVerticalAngle)
            {
                _xAxisRotation = _maxVerticalAngle;
                _mouseY = 0.0f;
                Player.CameraTransform.ClampRotation(-_maxVerticalAngle);
            }
            else if (_xAxisRotation < _minVerticalAngle)
            {
                _xAxisRotation = _minVerticalAngle;
                _mouseY = 0.0f;
                Player.CameraTransform.ClampRotation(-_minVerticalAngle);
            }
        }

        #endregion

        #region Private Methods

        private void SetReferences()
        {
            Controller = GetComponentInParent<CharacterController>();
            StateMachine = GetComponent<PlayerStateMachine>();
            Input = GetComponentInParent<PlayerInput>();

            _context = GetComponentInParent<PlayerController>();
        }

        #endregion
    }
}
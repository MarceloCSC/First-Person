using An01malia.FirstPerson.Core;
using An01malia.FirstPerson.Core.References;
using UnityEngine;

namespace An01malia.FirstPerson.PlayerModule
{
    public class PlayerCamera : MonoBehaviour
    {
        #region Fields

        [SerializeField] private float _mouseSensitivity = 15.0f;
        [SerializeField] private float _maxVerticalAngle = 90.0f;
        [SerializeField] private float _minVerticalAngle = -75.0f;

        private float _mouseX;
        private float _mouseY;
        private float _xAxisRotation;

        private PlayerInput _playerInput;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            _playerInput = GetComponent<PlayerInput>();
        }

        #endregion

        #region Public Methods

        public void UpdateCamera()
        {
            _mouseX = _playerInput.ViewInputValues.x * _mouseSensitivity * Time.deltaTime;
            _mouseY = _playerInput.ViewInputValues.y * _mouseSensitivity * Time.deltaTime;

            Player.CameraTransform.Rotate(Vector3.left * _mouseY);
            Player.Transform.Rotate(Vector3.up * _mouseX);

            ClampVerticalAngle();
        }

        #endregion

        #region Private Methods

        private void ClampVerticalAngle()
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
    }
}
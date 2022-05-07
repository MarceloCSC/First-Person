using UnityEngine;

namespace FirstPerson.Player
{
    public class PlayerCamera : MonoBehaviour
    {
        #region Fields

        [SerializeField] private Transform _cameraTransform;
        [SerializeField] private float _mouseSensitivity = 15.0f;
        [SerializeField] private float _maxVerticalAngle = 90.0f;
        [SerializeField] private float _minVerticalAngle = -75.0f;

        private float _mouseX;
        private float _mouseY;
        private float _xAxisRotation;
        private Vector3 _cameraPosition;

        private PlayerInputManager _inputManager;

        #endregion

        #region Properties

        public Transform CameraTransform => _cameraTransform;
        public Vector3 CameraPosition => _cameraPosition;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            _inputManager = GetComponent<PlayerInputManager>();
            _cameraPosition = _cameraTransform.localPosition;
        }

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        #endregion

        #region Public Methods

        public void UpdateCamera()
        {
            _mouseX = _inputManager.CameraInputValues.x * _mouseSensitivity * Time.deltaTime;
            _mouseY = _inputManager.CameraInputValues.y * _mouseSensitivity * Time.deltaTime;

            _cameraTransform.Rotate(Vector3.left * _mouseY);
            transform.Rotate(Vector3.up * _mouseX);

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
                _cameraTransform.eulerAngles = ClampRotation(-_maxVerticalAngle);
            }
            else if (_xAxisRotation < _minVerticalAngle)
            {
                _xAxisRotation = _minVerticalAngle;
                _mouseY = 0.0f;
                _cameraTransform.eulerAngles = ClampRotation(-_minVerticalAngle);
            }
        }

        private Vector3 ClampRotation(float xValue = 0, float yValue = 0, float zValue = 0)
        {
            Vector3 eulerRotation = _cameraTransform.eulerAngles;

            eulerRotation.x = xValue == 0 ? _cameraTransform.eulerAngles.x : xValue;
            eulerRotation.y = yValue == 0 ? _cameraTransform.eulerAngles.y : yValue;
            eulerRotation.z = zValue == 0 ? _cameraTransform.eulerAngles.z : zValue;

            return eulerRotation;
        }

        #endregion
    }
}
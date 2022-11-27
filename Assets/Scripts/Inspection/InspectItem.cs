using UnityEngine;

namespace An01malia.FirstPerson.Inspection
{
    public class InspectItem : MonoBehaviour
    {
        #region Fields

        [SerializeField] private float _initialFOV = 60.0f;
        [SerializeField] private float _minZoom = 30.0f;
        [SerializeField] private float _maxZoom = 100.0f;
        [SerializeField] private float _zoomSpeed = 500.0f;
        [SerializeField] private float _rotationSpeed = 5.0f;

        public static Camera InspectionCamera;
        public static Transform ItemPlacement;

        private InputActions _actions;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            _actions = new InputActions();
        }

        private void OnEnable()
        {
            _actions.Inspection.Enable();
            transform.position = ItemPlacement.position;
            transform.LookAt(InspectionCamera.transform);
            InspectionCamera.fieldOfView = _initialFOV;
        }

        private void Update()
        {
            MouseScrollZoom();
        }

        private void OnMouseDrag()
        {
            float inputX = _actions.Inspection.Rotate.ReadValue<Vector2>().x * _rotationSpeed;
            float inputY = _actions.Inspection.Rotate.ReadValue<Vector2>().y * _rotationSpeed;

            transform.Rotate(Vector3.right, inputY, Space.World);
            transform.Rotate(Vector3.up, -inputX, Space.World);
        }

        private void OnDisable()
        {
            _actions.Inspection.Disable();
        }

        #endregion

        #region Private Methods

        private void MouseScrollZoom()
        {
            if (_actions.Inspection.Zoom.ReadValue<Vector2>().y > 0)
            {
                InspectionCamera.fieldOfView -= _zoomSpeed * Time.deltaTime;
            }
            else if (_actions.Inspection.Zoom.ReadValue<Vector2>().y < 0)
            {
                InspectionCamera.fieldOfView += _zoomSpeed * Time.deltaTime;
            }

            InspectionCamera.fieldOfView = Mathf.Clamp(InspectionCamera.fieldOfView, _minZoom, _maxZoom);
        }

        #endregion
    }
}
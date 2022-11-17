using UnityEngine;

namespace An01malia.FirstPerson.Inspection
{
    public class InspectItem : MonoBehaviour
    {
        [SerializeField] private float initialFOV = 60.0f;
        [SerializeField] private float minZoom = 30.0f;
        [SerializeField] private float maxZoom = 100.0f;
        [SerializeField] private float zoomSpeed = 500.0f;
        [SerializeField] private float rotationSpeed = 5.0f;

        public static Camera ExamineCamera;
        public static Transform ExamineSpot;

        private InputActions _actions;

        private void Awake()
        {
            _actions = new InputActions();
        }

        private void OnEnable()
        {
            _actions.Examine.Enable();
            transform.position = ExamineSpot.position;
            transform.LookAt(ExamineCamera.transform);
            ExamineCamera.fieldOfView = initialFOV;
        }

        private void OnDisable()
        {
            _actions.Examine.Disable();
        }

        private void Update()
        {
            MouseScrollZoom();
        }

        private void MouseScrollZoom()
        {
            if (_actions.Examine.Zoom.ReadValue<Vector2>().y > 0)
            {
                ExamineCamera.fieldOfView -= zoomSpeed * Time.deltaTime;
            }
            else if (_actions.Examine.Zoom.ReadValue<Vector2>().y < 0)
            {
                ExamineCamera.fieldOfView += zoomSpeed * Time.deltaTime;
            }

            ExamineCamera.fieldOfView = Mathf.Clamp(ExamineCamera.fieldOfView, minZoom, maxZoom);
        }

        private void OnMouseDrag()
        {
            float inputX = _actions.Examine.Rotate.ReadValue<Vector2>().x * rotationSpeed;
            float inputY = _actions.Examine.Rotate.ReadValue<Vector2>().y * rotationSpeed;

            transform.Rotate(Vector3.right, inputY, Space.World);
            transform.Rotate(Vector3.up, -inputX, Space.World);
        }
    }
}
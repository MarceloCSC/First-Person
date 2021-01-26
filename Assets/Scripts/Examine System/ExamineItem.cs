using UnityEngine;

namespace FirstPerson.Examine
{

    public class ExamineItem : MonoBehaviour
    {

        [SerializeField] float initialFOV = 60.0f;
        [SerializeField] float minZoom = 30.0f;
        [SerializeField] float maxZoom = 100.0f;
        [SerializeField] float zoomSpeed = 500.0f;
        [SerializeField] float rotationSpeed = 5.0f;

        public static Camera ExamineCamera;
        public static Transform ExamineSpot;


        private void OnEnable()
        {
            transform.position = ExamineSpot.position;
            transform.LookAt(ExamineCamera.transform);
            ExamineCamera.fieldOfView = initialFOV;
        }

        private void Update()
        {
            MouseScrollZoom();
        }

        private void MouseScrollZoom()
        {
            if (Input.GetAxis(Mouse.ScrollWheel) > 0)
            {
                ExamineCamera.fieldOfView -= zoomSpeed * Time.deltaTime;
            }
            else if (Input.GetAxis(Mouse.ScrollWheel) < 0)
            {
                ExamineCamera.fieldOfView += zoomSpeed * Time.deltaTime;
            }

            ExamineCamera.fieldOfView = Mathf.Clamp(ExamineCamera.fieldOfView, minZoom, maxZoom);
        }

        private void OnMouseDrag()
        {
            float mouseX = Input.GetAxis(Mouse.X) * rotationSpeed;
            float mouseY = Input.GetAxis(Mouse.Y) * rotationSpeed;

            transform.Rotate(Vector3.right, mouseY, Space.World);
            transform.Rotate(Vector3.up, -mouseX, Space.World);
        }

    }

}

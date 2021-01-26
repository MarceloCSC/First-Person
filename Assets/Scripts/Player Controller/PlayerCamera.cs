using UnityEngine;

namespace FirstPerson.PlayerController
{

    [System.Serializable]
    public struct ClampedAxis
    {
        public float maxAngle;
        public float minAngle;
    }

    public abstract class PlayerCamera : MonoBehaviour
    {

        [SerializeField] protected float mouseSensitivity = 150.0f;
        [SerializeField] protected ClampedAxis verticalAxis = default;
        [SerializeField] protected ClampedAxis horizontalAxis = default;

        protected float mouseX;
        protected float mouseY;
        protected float xRotation;
        protected float yRotation;

        protected abstract void CameraRotation();
     
    }

}

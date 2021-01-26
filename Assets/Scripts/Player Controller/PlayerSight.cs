using UnityEngine;

namespace FirstPerson.PlayerController
{

    public class PlayerSight : PlayerCamera
    {

        public static bool SightLocked { get; set; }


        private void Awake()
        {
            Player.LockCursor(true);
        }

        private void Update()
        {
            if (!SightLocked)
            {
                mouseX = Input.GetAxis(Mouse.X) * mouseSensitivity * Time.deltaTime;
                mouseY = Input.GetAxis(Mouse.Y) * mouseSensitivity * Time.deltaTime;

                CameraRotation();

                References.PlayerSight.Rotate(Vector3.left * mouseY);
                References.PlayerTransform.Rotate(Vector3.up * mouseX);
            }
        }

        protected override void CameraRotation()
        {
            xRotation += mouseY;

            if (xRotation > verticalAxis.maxAngle)
            {
                xRotation = verticalAxis.maxAngle;
                mouseY = 0.0f;
                References.PlayerSight.ClampRotation(-verticalAxis.maxAngle);
            }
            else if (xRotation < verticalAxis.minAngle)
            {
                xRotation = verticalAxis.minAngle;
                mouseY = 0.0f;
                References.PlayerSight.ClampRotation(-verticalAxis.minAngle);
            }
        }

    }

}

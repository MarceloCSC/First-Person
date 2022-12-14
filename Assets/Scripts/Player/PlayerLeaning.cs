using UnityEngine;
using An01malia.FirstPerson.UIModule;

namespace An01malia.FirstPerson.PlayerModule
{

    //    public class PlayerLeaning : PlayerCamera
    //    {

    //        [SerializeField] float turningRate = 100.0f;
    //        [SerializeField] bool enableLeaning = true;
    //        [SerializeField] bool enablePanning = true;
    //        [SerializeField] ClampedAxis leaningAxis = default;
    //        [SerializeField] ClampedAxis verticalCenter = default;


    //        private float leanRotation;
    //        private bool isLeaning = false;


    //        #region Properties
    //        private bool IsLeaning
    //        {
    //            get => isLeaning;
    //            set
    //            {
    //                if (isLeaning == value) { return; }
    //                Player.LockIntoPlace(value);
    //                isLeaning = value;
    //            }
    //        }
    //        #endregion


    //        private void Update()
    //        {
    //            if (enableLeaning && !PlayerMovement.IsJumping && !UIPanels.IsOnScreen)
    //            {
    //                if (Input.GetButton(Control.Lean) && IsCentered())
    //                {
    //                    IsLeaning = true;
    //                    Lean();
    //                }
    //                else if (IsLeaning)
    //                {
    //                    LeanBack();
    //                }
    //            }
    //        }

    //        private void Lean()
    //        {
    //            leanRotation -= Input.GetAxis(Control.Lean) * turningRate * Time.deltaTime;
    //            leanRotation = Mathf.Clamp(leanRotation, leaningAxis.minAngle, leaningAxis.maxAngle);

    //            References.LeaningAxis.localRotation = Quaternion.Euler(0.0f, 0.0f, leanRotation);

    //            if (enablePanning && Mathf.Abs(leanRotation) == leaningAxis.maxAngle)
    //            {
    //                CameraRotation();
    //            }
    //        }

    //        private void LeanBack()
    //        {
    //            References.LeaningAxis.AlignRotation(out Quaternion axisRotation, turningRate);

    //            leanRotation -= Mathf.Sign(axisRotation.z) * turningRate * Time.deltaTime;

    //            References.CameraTransform.AlignRotation(out Quaternion cameraRotation, turningRate);

    //            if (axisRotation == Quaternion.Euler(Vector3.zero) && cameraRotation == Quaternion.Euler(Vector3.zero))
    //            {
    //                xRotation = 0.0f;
    //                yRotation = 0.0f;
    //                leanRotation = 0.0f;
    //                IsLeaning = false;
    //            }
    //        }

    //        protected override void CameraRotation()
    //        {
    //            mouseX = Input.GetAxis(Mouse.X) * mouseSensitivity * Time.deltaTime;
    //            mouseY = Input.GetAxis(Mouse.Y) * mouseSensitivity * Time.deltaTime;

    //            xRotation -= mouseY;
    //            yRotation += mouseX;

    //            xRotation = Mathf.Clamp(xRotation, verticalAxis.minAngle, verticalAxis.maxAngle);
    //            yRotation = Mathf.Clamp(yRotation, horizontalAxis.minAngle, horizontalAxis.maxAngle);

    //            References.CameraTransform.localRotation = Quaternion.Euler(xRotation, yRotation, 0.0f);
    //        }

    //        private bool IsCentered()
    //        {
    //            return References.PlayerSight.eulerAngles.x > 360.0f - verticalCenter.maxAngle
    //                     || References.PlayerSight.eulerAngles.x < -verticalCenter.minAngle;
    //        }

    //    }

}

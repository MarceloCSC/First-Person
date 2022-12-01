using An01malia.FirstPerson.Core.References;
using An01malia.FirstPerson.PlayerModule.States.Data;
using An01malia.FirstPerson.PlayerModule.States.DTOs;
using UnityEngine;

namespace An01malia.FirstPerson.PlayerModule.States
{
    public class PlayerInspectState : PlayerBaseState
    {
        #region Fields

        [SerializeField] private float _initialFOV = 60.0f;
        [SerializeField] private float _minZoom = 30.0f;
        [SerializeField] private float _maxZoom = 100.0f;
        [SerializeField] private float _zoomSpeed = 500.0f;
        [SerializeField] private float _rotationSpeed = 5.0f;

        #endregion

        #region Overriden Methods

        public override void EnterState(PlayerActionDTO dto)
        {
            StateData = new PlayerStateData(dto);

            StateData.Item.position = UI.ItemPlacement.position;
            StateData.Item.LookAt(UI.InspectionCamera.transform);
            UI.InspectionCamera.fieldOfView = _initialFOV;
        }

        public override PlayerActionDTO ExitState()
        {
            return StateData.GetData();
        }

        public override void UpdateState()
        {
            HandleZoom();
        }

        public override void UpdateCamera()
        {
        }

        public override void CheckSwitchState()
        {
        }

        public override void TriggerSwitchState(ActionType action, ActionDTO dto = null)
        {
            base.TriggerSwitchState(action, dto);

            switch (action)
            {
                case ActionType.Inspect:
                    break;

                default:
                    break;
            }
        }

        #endregion

        #region Unity Methods

        private void OnMouseDrag()
        {
            float inputX = Input.RotationInputValues.x * _rotationSpeed;
            float inputY = Input.RotationInputValues.y * _rotationSpeed;

            StateData.Item.Rotate(Vector3.right, inputY, Space.World);
            StateData.Item.Rotate(Vector3.up, -inputX, Space.World);
        }

        #endregion

        #region Private Methods

        private void HandleZoom()
        {
            if (Input.ZoomInputValues.y > 0)
            {
                UI.InspectionCamera.fieldOfView -= _zoomSpeed * Time.deltaTime;
            }
            else if (Input.ZoomInputValues.y < 0)
            {
                UI.InspectionCamera.fieldOfView += _zoomSpeed * Time.deltaTime;
            }

            UI.InspectionCamera.fieldOfView = Mathf.Clamp(UI.InspectionCamera.fieldOfView, _minZoom, _maxZoom);
        }

        #endregion
    }
}
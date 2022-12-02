using An01malia.FirstPerson.Core;
using An01malia.FirstPerson.Core.References;
using An01malia.FirstPerson.InteractionModule;
using An01malia.FirstPerson.InventoryModule.Items;
using An01malia.FirstPerson.PlayerModule.States.Data;
using An01malia.FirstPerson.PlayerModule.States.DTOs;
using An01malia.FirstPerson.UIModule;
using UnityEngine;

namespace An01malia.FirstPerson.PlayerModule.States
{
    public class PlayerInspectState : PlayerBaseState
    {
        #region Fields

        [SerializeField] private float _initialFOV = 60.0f;
        [SerializeField] private float _minZoom = 30.0f;
        [SerializeField] private float _maxZoom = 100.0f;
        [SerializeField] private float _zoomSpeed = 1000.0f;
        [SerializeField] private float _rotationSpeed = 5.0f;

        public static GameObject Panel;

        private GameObject _item;

        #endregion

        #region Overriden Methods

        public override void EnterState(PlayerActionDTO dto)
        {
            StateData = new PlayerStateData(dto);

            ActivateUI();
            SetItemToInspect();
            ToggleInGameItem(false);
        }

        public override PlayerActionDTO ExitState()
        {
            DeactivateUI();
            RemoveItemToInspect();
            ToggleInGameItem(true);

            StateData = new PlayerStateData();

            return StateData.GetData();
        }

        public override void UpdateState()
        {
            HandleRotation();
        }

        public override void UpdateCamera()
        {
            HandleZoom();
        }

        public override void CheckSwitchState()
        {
        }

        public override void TriggerSwitchState(ActionType action, ActionDTO dto = null)
        {
            base.TriggerSwitchState(action, dto);

            switch (action)
            {
                case ActionType.None:
                    SwitchState(StateMachine.Idle());
                    break;

                default:
                    break;
            }
        }

        #endregion

        #region Private Methods

        private void HandleRotation()
        {
            float inputX = Input.RotationInputValues.x * _rotationSpeed;
            float inputY = Input.RotationInputValues.y * _rotationSpeed;

            _item.transform.Rotate(Vector3.back, -inputY, Space.World);
            _item.transform.Rotate(Vector3.up, -inputX, Space.World);
        }

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

        private void ActivateUI()
        {
            GameStateManager.Instance.ChangeState(GameState.UI);

            UIPanelManager.CloseOpenPanels();
            UIPanelManager.ToggleUIPanel(Panel);

            UI.InspectionCamera.gameObject.SetActive(true);
            UI.LightSource.SetActive(true);
            UI.InspectionCamera.fieldOfView = _initialFOV;
        }

        private void DeactivateUI()
        {
            GameStateManager.Instance.ChangeState(GameState.UI);

            UIPanelManager.ToggleUIPanel(Panel);

            UI.InspectionCamera.gameObject.SetActive(false);
            UI.LightSource.SetActive(false);
        }

        private void SetItemToInspect()
        {
            if (TrySetItem())
            {
                _item.SetActive(true);
                _item.transform.position = UI.ItemPlacement.position;
                _item.transform.LookAt(UI.InspectionCamera.transform);
            }
        }

        private void RemoveItemToInspect()
        {
            _item.SetActive(false);
            _item = null;
        }

        private bool TrySetItem() => StateData.Item.TryGetComponent(out Inspectable inspectable) &&
                                        ItemPooler.Instance.ItemsToExamine.TryGetValue(inspectable.Item.ID, out _item);

        private void ToggleInGameItem(bool isActive) => StateData.Item.gameObject.SetActive(isActive);

        #endregion
    }
}
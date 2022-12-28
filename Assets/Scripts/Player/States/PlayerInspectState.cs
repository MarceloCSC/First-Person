using An01malia.FirstPerson.Core;
using An01malia.FirstPerson.ItemModule;
using An01malia.FirstPerson.ItemModule.Items;
using An01malia.FirstPerson.PlayerModule.States.Data;
using An01malia.FirstPerson.PlayerModule.States.DTOs;
using An01malia.FirstPerson.UserInterfaceModule;
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

        public override bool TrySwitchState(ActionType action, ActionDTO dto = null)
        {
            if (base.TrySwitchState(action, dto)) return false;

            switch (action)
            {
                case ActionType.None:
                    SwitchState(StateMachine.Idle());
                    return true;

                default:
                    return false;
            }
        }

        #endregion

        #region Private Methods

        private void HandleRotation()
        {
            float inputX = PlayerInput.RotationInputValues.x * _rotationSpeed;
            float inputY = PlayerInput.RotationInputValues.y * _rotationSpeed;

            _item.transform.Rotate(Vector3.back, -inputY, Space.World);
            _item.transform.Rotate(Vector3.up, -inputX, Space.World);
        }

        private void HandleZoom()
        {
            if (PlayerInput.ZoomInputValues.y > 0)
            {
                Player.InspectionCamera.fieldOfView -= _zoomSpeed * Time.deltaTime;
            }
            else if (PlayerInput.ZoomInputValues.y < 0)
            {
                Player.InspectionCamera.fieldOfView += _zoomSpeed * Time.deltaTime;
            }

            Player.InspectionCamera.fieldOfView = Mathf.Clamp(Player.InspectionCamera.fieldOfView, _minZoom, _maxZoom);
        }

        private void ActivateUI()
        {
            GameStateManager.Instance.ChangeState(GameState.UI);

            UIPanelManager.CloseOpenPanels();
            UIPanelManager.ToggleUIPanel(Panel);

            Player.InspectionCamera.gameObject.SetActive(true);
            Player.InspectionCamera.transform.SetPositionAndRotation(Player.Camera.position, Player.Camera.rotation);
            Player.InspectionLightSource.SetActive(true);
            Player.InspectionCamera.fieldOfView = _initialFOV;
        }

        private void DeactivateUI()
        {
            GameStateManager.Instance.ChangeState(GameState.UI);

            UIPanelManager.ToggleUIPanel(Panel);

            Player.InspectionCamera.gameObject.SetActive(false);
            Player.InspectionLightSource.SetActive(false);
        }

        private void SetItemToInspect()
        {
            if (TrySetItem())
            {
                _item.SetActive(true);
                _item.transform.position = Player.InspectionItemPlacement.position;
                _item.transform.LookAt(Player.InspectionCamera.transform);
            }
        }

        private void RemoveItemToInspect()
        {
            _item.SetActive(false);
            _item = null;
        }

        private bool TrySetItem() => StateData.Transform.TryGetComponent(out ItemToInspect item) &&
                                        ItemPooler.Instance.ItemsToExamine.TryGetValue(item.Root.ID, out _item);

        private void ToggleInGameItem(bool isActive) => StateData.Transform.gameObject.SetActive(isActive);

        #endregion
    }
}
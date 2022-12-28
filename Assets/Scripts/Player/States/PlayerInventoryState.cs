using An01malia.FirstPerson.Core;
using An01malia.FirstPerson.InteractionModule.Interactive;
using An01malia.FirstPerson.InventoryModule;
using An01malia.FirstPerson.PlayerModule.States.Data;
using An01malia.FirstPerson.PlayerModule.States.DTOs;
using An01malia.FirstPerson.UserInterfaceModule;
using UnityEngine;

namespace An01malia.FirstPerson.PlayerModule.States
{
    public class PlayerInventoryState : PlayerBaseState
    {
        #region Overriden Methods

        public override void EnterState(PlayerActionDTO dto)
        {
            StateData = new PlayerStateData(dto);

            HandleInventory();
        }

        public override PlayerActionDTO ExitState()
        {
            HandleInventory();

            StateData = new PlayerStateData();

            return StateData.GetData();
        }

        public override void UpdateState()
        {
        }

        public override void UpdateCamera()
        {
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

                case ActionType.Inventory:
                    SwitchState(StateMachine.Idle());
                    return true;

                default:
                    return false;
            }
        }

        #endregion

        #region Private Methods

        private void HandleInventory()
        {
            if (IsContainer(out StorageUnit storage))
            {
                storage.StartInteraction();

                ToggleUIPanels(PlayerInventory.Panel, Container.Panel);
            }
            else
            {
                ToggleUIPanels(PlayerInventory.Panel);
            }
        }

        private void ToggleUIPanels(GameObject inventory, GameObject container = null)
        {
            GameStateManager.Instance.ChangeState(GameState.UI);

            UIPanelManager.ToggleUIPanel(inventory);

            if (container) UIPanelManager.ToggleUIPanel(container);
        }

        private bool IsContainer(out StorageUnit storage)
        {
            storage = null;

            return StateData.Transform != null && StateData.Transform.TryGetComponent(out storage);
        }

        #endregion
    }
}
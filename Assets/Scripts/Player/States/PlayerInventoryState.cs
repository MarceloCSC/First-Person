using An01malia.FirstPerson.Core;
using An01malia.FirstPerson.InteractionModule;
using An01malia.FirstPerson.InventoryModule;
using An01malia.FirstPerson.PlayerModule.States.Data;
using An01malia.FirstPerson.PlayerModule.States.DTOs;
using An01malia.FirstPerson.UIModule;
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

        public override void TriggerSwitchState(ActionType action, ActionDTO dto = null)
        {
            base.TriggerSwitchState(action, dto);

            switch (action)
            {
                case ActionType.Inventory:
                    SwitchState(StateMachine.Idle());
                    break;

                default:
                    break;
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

            return StateData.Item != null && StateData.Item.TryGetComponent(out storage);
        }

        #endregion
    }
}
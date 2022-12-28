using An01malia.FirstPerson.ItemModule;
using An01malia.FirstPerson.ItemModule.Items;
using An01malia.FirstPerson.PlayerModule.States.Data;
using An01malia.FirstPerson.PlayerModule.States.DTOs;

namespace An01malia.FirstPerson.PlayerModule.States
{
    public class PlayerCarryState : BaseState
    {
        #region Overriden Methods

        public override void EnterState(PlayerActionDTO dto)
        {
            StateData = new PlayerStateData(dto);

            CarryItem();
        }

        public override PlayerActionDTO ExitState()
        {
            DropItem();

            return null;
        }

        public override void UpdateState()
        {
            CheckSwitchState();
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
                    SuperState.RemoveSubState();
                    return true;

                case ActionType.Push:
                    SuperState.RemoveSubState();
                    return true;

                case ActionType.Climb:
                    SuperState.RemoveSubState();
                    return true;

                case ActionType.Carry:
                    SuperState.RemoveSubState();
                    return true;

                case ActionType.Interact when dto is ItemStandActionDTO actionDto:
                    return TryPlaceItem(actionDto.ItemStand);

                default:
                    return false;
            }
        }

        #endregion

        #region Private Methods

        private void CarryItem()
        {
            if (StateData.Transform.TryGetComponent(out ItemToCarry item))
            {
                item.Carry();
            }
        }

        private void DropItem()
        {
            if (StateData.Transform.TryGetComponent(out ItemToCarry item))
            {
                item.Drop();
            }
        }

        private bool TryPlaceItem(ItemStand itemStand)
        {
            if (!itemStand.TryPlaceItem(StateData.Transform)) return false;

            SuperState.RemoveSubState();

            return true;
        }

        #endregion
    }
}
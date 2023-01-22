using An01malia.FirstPerson.InteractionModule.Environment;
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

        public override PlayerActionDTO ExitState() => null;

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
                    DropItem();
                    RemoveState();
                    return true;

                case ActionType.Push:
                    DropItem();
                    RemoveState();
                    return true;

                case ActionType.Climb:
                    DropItem();
                    RemoveState();
                    return true;

                case ActionType.Carry:
                    DropItem();
                    RemoveState();
                    return true;

                case ActionType.Inspect when StateData.Transform.TryGetComponent(out ItemToInspect _):
                    SuperState.PushState(StateMachine.Inspect(), new TransformActionDTO(StateData.Transform));
                    return true;

                case ActionType.Interact when dto is ItemSpotActionDTO actionDto:
                    return TryPlaceItem(actionDto.ItemSpot);

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

        private bool TryPlaceItem(ItemSpot itemSpot)
        {
            if (!itemSpot.TryPlaceItem(StateData.Transform)) return false;

            RemoveState();

            return true;
        }

        #endregion
    }
}
using An01malia.FirstPerson.Core.References;
using An01malia.FirstPerson.PlayerModule.States.Data;
using An01malia.FirstPerson.PlayerModule.States.DTOs;
using UnityEngine;

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

        public override void TriggerSwitchState(ActionType action, ActionDTO dto = null)
        {
            base.TriggerSwitchState(action, dto);

            switch (action)
            {
                case ActionType.None:
                    SuperState.RemoveSubState();
                    break;

                case ActionType.Push:
                    SuperState.RemoveSubState();
                    break;

                case ActionType.Climb:
                    SuperState.RemoveSubState();
                    break;

                case ActionType.Carry:
                    SuperState.RemoveSubState();
                    break;

                case ActionType.Interact:
                    if (dto is InteractiveActionDTO interactive)
                    {
                        PlaceItem(interactive);
                    }
                    break;

                default:
                    break;
            }
        }

        #endregion

        #region Private Methods

        private void CarryItem()
        {
            StateData.Transform.GetComponent<Rigidbody>().isKinematic = true;
            StateData.Transform.GetComponent<Collider>().enabled = false;
            StateData.Transform.parent = Player.Hand;
            StateData.Transform.localPosition = Vector3.zero;
            StateData.Transform.eulerAngles = Vector3.zero;
        }

        private void DropItem()
        {
            StateData.Transform.GetComponent<Rigidbody>().isKinematic = false;
            StateData.Transform.GetComponent<Collider>().enabled = true;
            StateData.Transform.parent = null;
        }

        private void PlaceItem(InteractiveActionDTO dto)
        {
            print("placing item");

            dto.Interactive.StartInteraction();
        }

        #endregion
    }
}
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
                    (dto as InteractiveActionDTO).Interactive.StartInteraction();
                    break;

                default:
                    break;
            }
        }

        #endregion

        #region Private Methods

        private void CarryItem()
        {
            StateData.Item.GetComponent<Rigidbody>().isKinematic = true;
            StateData.Item.GetComponent<Collider>().enabled = false;
            StateData.Item.parent = Player.Hand;
            StateData.Item.localPosition = Vector3.zero;
            StateData.Item.eulerAngles = Vector3.zero;
        }

        private void DropItem()
        {
            StateData.Item.GetComponent<Rigidbody>().isKinematic = false;
            StateData.Item.GetComponent<Collider>().enabled = true;
            StateData.Item.parent = null;
        }

        #endregion
    }
}
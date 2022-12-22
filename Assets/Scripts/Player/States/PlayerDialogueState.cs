using An01malia.FirstPerson.DialogueModule;
using An01malia.FirstPerson.InteractionModule.Interactive;
using An01malia.FirstPerson.PlayerModule.States.Data;
using An01malia.FirstPerson.PlayerModule.States.DTOs;

namespace An01malia.FirstPerson.PlayerModule.States
{
    public class PlayerDialogueState : PlayerBaseState
    {
        #region Fields

        private NPC _character;

        #endregion

        #region Overriden Methods

        public override void EnterState(PlayerActionDTO dto)
        {
            StateData = new PlayerStateData(dto);

            EnterDialogue();
        }

        public override PlayerActionDTO ExitState()
        {
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
                case ActionType.Jump:
                    HandleDialogue();
                    break;

                case ActionType.Dialogue:
                    HandleDialogue();
                    break;

                default:
                    break;
            }
        }

        #endregion

        #region Private Methods

        private void EnterDialogue()
        {
            if (StateData.Transform.TryGetComponent(out _character))
            {
                _character.StartInteraction();
            }
        }

        private void HandleDialogue()
        {
            DialogueManager.Instance.HandleDialogue();

            if (DialogueManager.Instance.CanQuitDialogue)
            {
                SwitchState(StateMachine.Idle());
            }
        }

        #endregion
    }
}
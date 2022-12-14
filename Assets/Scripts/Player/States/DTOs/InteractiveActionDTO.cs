using An01malia.FirstPerson.InteractionModule.Interactive;

namespace An01malia.FirstPerson.PlayerModule.States.DTOs
{
    public class InteractiveActionDTO : ActionDTO
    {
        #region Properties

        public IInteractive Interactive { get; }

        #endregion

        #region Constructor

        public InteractiveActionDTO(IInteractive interactive)
        {
            Interactive = interactive;
        }

        #endregion
    }
}
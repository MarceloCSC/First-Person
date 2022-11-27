namespace An01malia.FirstPerson.PlayerModule.States.DTOs
{
    public class RunActionDTO : ActionDTO
    {
        #region Properties

        public bool IsRunPressed { get; }

        #endregion

        #region Constructor

        public RunActionDTO(bool isRunPressed)
        {
            IsRunPressed = isRunPressed;
        }

        #endregion
    }
}
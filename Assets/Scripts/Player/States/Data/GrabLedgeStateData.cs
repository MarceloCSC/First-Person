using An01malia.FirstPerson.PlayerModule.States.DTOs;

namespace An01malia.FirstPerson.PlayerModule.States.Data
{
    public class GrabLedgeStateData : PlayerStateData
    {
        #region Properties

        public bool IsGrabbing { get; set; }

        #endregion

        #region Constructor

        public GrabLedgeStateData(PlayerActionDTO dto) : base(dto)
        {
            IsGrabbing = true;
        }

        #endregion
    }
}
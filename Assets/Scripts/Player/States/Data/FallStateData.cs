using An01malia.FirstPerson.PlayerModule.States.DTOs;

namespace An01malia.FirstPerson.PlayerModule.States.Data
{
    public class FallStateData : PlayerStateData
    {
        #region Properties

        public int JumpsRemaining { get; set; } = 1;
        public float TimeFalling { get; set; }
        public float CoyoteTimeCounter { get; set; }

        #endregion

        #region Constructor

        public FallStateData(PlayerActionDTO dto) : base(dto)
        {
            if (dto is JumpPlayerActionDTO actionDto)
            {
                JumpsRemaining = actionDto.JumpsRemaining;
            }
        }

        #endregion
    }
}
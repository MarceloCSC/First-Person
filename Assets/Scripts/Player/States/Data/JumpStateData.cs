using An01malia.FirstPerson.PlayerModule.States.DTOs;

namespace An01malia.FirstPerson.PlayerModule.States.Data
{
    public class JumpStateData : PlayerStateData
    {
        #region Properties

        public int JumpsRemaining { get; set; }
        public float TimeInAir { get; set; }

        #endregion

        #region Constructor

        public JumpStateData(int jumps, PlayerActionDTO dto) : base(dto)
        {
            JumpsRemaining = jumps;
        }

        #endregion

        #region Public Methods

        public override PlayerActionDTO GetData() => new JumpPlayerActionDTO(JumpsRemaining, Speed, IsRunPressed, Momentum, Item);

        #endregion
    }
}
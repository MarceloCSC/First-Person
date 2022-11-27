using An01malia.FirstPerson.PlayerModule.States.DTOs;
using UnityEngine;

namespace An01malia.FirstPerson.PlayerModule.States.Data
{
    public class CrouchStateData : PlayerStateData
    {
        #region Properties

        public float StandingHeight { get; set; }
        public Vector3 InitialPosition { get; set; }
        public Coroutine Coroutine { get; set; }

        #endregion

        #region Constructor

        public CrouchStateData(float height, Vector3 initialPosition, PlayerActionDTO dto) : base(dto)
        {
            StandingHeight = height;
            InitialPosition = initialPosition;
        }

        #endregion
    }
}
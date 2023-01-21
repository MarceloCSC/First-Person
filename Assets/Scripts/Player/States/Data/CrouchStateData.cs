using An01malia.FirstPerson.PlayerModule.States.DTOs;
using UnityEngine;

namespace An01malia.FirstPerson.PlayerModule.States.Data
{
    public class CrouchStateData : PlayerStateData
    {
        #region Properties

        public Vector3 SightPosition { get; set; }
        public Coroutine Coroutine { get; set; }

        #endregion

        #region Constructor

        public CrouchStateData(Vector3 sightPosition, PlayerActionDTO dto) : base(dto)
        {
            SightPosition = sightPosition;
        }

        #endregion
    }
}
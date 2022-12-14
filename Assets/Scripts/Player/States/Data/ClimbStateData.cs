using An01malia.FirstPerson.PlayerModule.States.DTOs;
using UnityEngine;

namespace An01malia.FirstPerson.PlayerModule.States.Data
{
    public class ClimbStateData : PlayerStateData
    {
        #region Properties

        public bool IsClimbing { get; set; }
        public bool CanClimbUpwards { get; set; }
        public bool CanClimbSideways { get; set; }
        public Vector3 SurfacePosition { get; set; }
        public Vector3 SurfaceDirection { get; set; }
        public Vector3 SurfaceRightAxis { get; set; }
        public Coroutine Coroutine { get; set; }

        #endregion

        #region Constructor

        public ClimbStateData(Vector3 surfacePosition, PlayerActionDTO dto) : base(dto)
        {
            SurfacePosition = surfacePosition;
        }

        #endregion
    }
}
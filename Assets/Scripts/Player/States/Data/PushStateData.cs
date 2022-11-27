using An01malia.FirstPerson.PlayerModule.States.DTOs;
using UnityEngine;

namespace An01malia.FirstPerson.PlayerModule.States.Data
{
    public class PushStateData : PlayerStateData
    {
        #region Properties

        public bool IsPushing { get; set; }
        public bool IsZAxisAligned { get; set; }
        public bool CanPushForward { get; set; }
        public bool CanPushSideways { get; set; }
        public float CurrentSpeed { get; set; }
        public float Acceleration { get; set; }
        public Vector3 PreviousInputVector { get; set; }
        public Vector3 DifferenceToPlayer { get; set; }
        public Vector3 FacingDirection { get; set; }
        public Vector3 ColliderSize { get; set; }
        public Coroutine Coroutine { get; set; }

        #endregion

        #region Constructor

        public PushStateData(Vector3 colliderSize, PlayerActionDTO dto) : base(dto)
        {
            ColliderSize = colliderSize;
        }

        #endregion
    }
}
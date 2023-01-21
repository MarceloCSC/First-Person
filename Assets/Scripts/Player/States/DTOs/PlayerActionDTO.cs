using UnityEngine;

namespace An01malia.FirstPerson.PlayerModule.States.DTOs
{
    public class PlayerActionDTO : ActionDTO
    {
        #region Properties

        public float Speed { get; }
        public bool IsRunPressed { get; }
        public bool IsCrouching { get; }
        public Vector3 Momentum { get; }
        public Transform Transform { get; }

        #endregion

        #region Constructor

        public PlayerActionDTO(float speed, bool isRunPressed, bool isCrouching, Vector3 momentum, Transform transform = null)
        {
            Speed = speed;
            IsRunPressed = isRunPressed;
            IsCrouching = isCrouching;
            Momentum = momentum != null ? momentum : Vector3.zero;
            Transform = transform;
        }

        #endregion
    }
}
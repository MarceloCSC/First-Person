using UnityEngine;

namespace An01malia.FirstPerson.PlayerModule.States.DTOs
{
    public class PlayerActionDTO : ActionDTO
    {
        #region Properties

        public float Speed { get; }
        public bool IsRunPressed { get; }
        public Vector3 Momentum { get; }
        public Transform Item { get; }

        #endregion

        #region Constructor

        public PlayerActionDTO(float speed, bool isRunPressed, Vector3 momentum, Transform item = null)
        {
            Speed = speed;
            IsRunPressed = isRunPressed;
            Momentum = momentum != null ? momentum : Vector3.zero;
            Item = item;
        }

        #endregion
    }
}
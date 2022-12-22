using An01malia.FirstPerson.PlayerModule.States.DTOs;
using UnityEngine;

namespace An01malia.FirstPerson.PlayerModule.States.Data
{
    public class PlayerStateData
    {
        #region Properties

        public float Speed { get; set; }
        public bool IsRunPressed { get; set; }
        public Vector3 Momentum { get; set; } = Vector3.zero;
        public Transform Transform { get; set; }

        #endregion

        #region Constructor

        public PlayerStateData()
        {
        }

        public PlayerStateData(float speed, bool isRunPressed, Vector3 momentum = default, Transform transform = null)
        {
            Speed = speed;
            IsRunPressed = isRunPressed;
            Momentum = momentum;
            Transform = transform;
        }

        public PlayerStateData(PlayerActionDTO dto)
        {
            IsRunPressed = dto.IsRunPressed;
            Speed = dto.Speed;
            Momentum = dto.Momentum;
            Transform = dto.Transform;
        }

        #endregion

        #region Public Methods

        public virtual void SetData(ActionDTO dto)
        {
            switch (dto)
            {
                case JumpPlayerActionDTO jumpDto:
                    IsRunPressed = jumpDto.IsRunPressed;
                    Speed = jumpDto.Speed;
                    Momentum = jumpDto.Momentum;
                    Transform = jumpDto.Transform ? jumpDto.Transform : Transform;
                    break;

                case PlayerActionDTO actionDto:
                    IsRunPressed = actionDto.IsRunPressed;
                    Speed = actionDto.Speed;
                    Momentum = actionDto.Momentum;
                    Transform = actionDto.Transform ? actionDto.Transform : Transform;
                    break;

                case RunActionDTO runDto:
                    IsRunPressed = runDto.IsRunPressed;
                    break;

                case MovementActionDTO movementDto:
                    Momentum = movementDto.Movement;
                    break;

                case TransformActionDTO transformDto:
                    Transform = transformDto.Transform;
                    break;
            }
        }

        public virtual PlayerActionDTO GetData()
        {
            return new PlayerActionDTO(Speed, IsRunPressed, Momentum, Transform);
        }

        #endregion
    }
}
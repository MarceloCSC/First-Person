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
        public Transform Item { get; set; }

        #endregion

        #region Constructor

        public PlayerStateData()
        {
        }

        public PlayerStateData(float speed, bool isRunPressed, Vector3 momentum = default, Transform item = null)
        {
            Speed = speed;
            IsRunPressed = isRunPressed;
            Momentum = momentum;
            Item = item;
        }

        public PlayerStateData(PlayerActionDTO dto)
        {
            IsRunPressed = dto.IsRunPressed;
            Speed = dto.Speed;
            Momentum = dto.Momentum;
            Item = dto.Item;
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
                    Item = jumpDto.Item ? jumpDto.Item : Item;
                    break;

                case PlayerActionDTO actionDto:
                    IsRunPressed = actionDto.IsRunPressed;
                    Speed = actionDto.Speed;
                    Momentum = actionDto.Momentum;
                    Item = actionDto.Item ? actionDto.Item : Item;
                    break;

                case RunActionDTO runDto:
                    IsRunPressed = runDto.IsRunPressed;
                    break;

                case MovementActionDTO movementDto:
                    Momentum = movementDto.Movement;
                    break;

                case ItemActionDTO itemDto:
                    Item = itemDto.Item;
                    break;
            }
        }

        public virtual PlayerActionDTO GetData()
        {
            return new PlayerActionDTO(Speed, IsRunPressed, Momentum, Item);
        }

        #endregion
    }
}
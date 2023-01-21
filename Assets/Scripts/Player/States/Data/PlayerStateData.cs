﻿using An01malia.FirstPerson.PlayerModule.States.DTOs;
using UnityEngine;

namespace An01malia.FirstPerson.PlayerModule.States.Data
{
    public class PlayerStateData
    {
        #region Properties

        public float Speed { get; set; }
        public bool IsRunPressed { get; set; }
        public bool IsCrouching { get; set; }
        public Vector3 Momentum { get; set; } = Vector3.zero;
        public Transform Transform { get; set; }

        #endregion

        #region Constructor

        public PlayerStateData()
        { }

        public PlayerStateData(float speed, bool isRunPressed, bool isCrouching, Vector3 momentum = default, Transform transform = null)
        {
            Speed = speed;
            IsRunPressed = isRunPressed;
            IsCrouching = isCrouching;
            Momentum = momentum;
            Transform = transform;
        }

        public PlayerStateData(PlayerActionDTO dto)
        {
            Speed = dto.Speed;
            IsRunPressed = dto.IsRunPressed;
            IsCrouching = dto.IsCrouching;
            Momentum = dto.Momentum;
            Transform = dto.Transform;
        }

        #endregion

        #region Public Methods

        public virtual void SetData(ActionDTO dto)
        {
            switch (dto)
            {
                case RunActionDTO runDto:
                    IsRunPressed = runDto.IsRunPressed;
                    break;

                case MovementActionDTO movementDto:
                    Momentum = movementDto.Movement;
                    break;

                case TransformActionDTO transformDto:
                    Transform = transformDto.Transform;
                    break;

                default:
                    if (dto is PlayerActionDTO actionDto)
                    {
                        Speed = actionDto.Speed;
                        IsRunPressed = actionDto.IsRunPressed;
                        IsCrouching = actionDto.IsCrouching;
                        Momentum = actionDto.Momentum;
                        Transform = actionDto.Transform ? actionDto.Transform : Transform;
                    }
                    break;
            }
        }

        public virtual PlayerActionDTO GetData()
        {
            return new PlayerActionDTO(Speed, IsRunPressed, IsCrouching, Momentum, Transform);
        }

        #endregion
    }
}
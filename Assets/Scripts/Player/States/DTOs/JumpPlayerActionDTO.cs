﻿using UnityEngine;

namespace An01malia.FirstPerson.PlayerModule.States.DTOs
{
    public class JumpPlayerActionDTO : PlayerActionDTO
    {
        #region Properties

        public int JumpsRemaining { get; }

        #endregion

        #region Constructor

        public JumpPlayerActionDTO(int jumpsRemaining,
                                   float speed,
                                   bool isRunPressed,
                                   Vector3 momentum = default,
                                   Transform item = null)

            : base(speed, isRunPressed, momentum, item)
        {
            JumpsRemaining = jumpsRemaining;
        }

        #endregion
    }
}
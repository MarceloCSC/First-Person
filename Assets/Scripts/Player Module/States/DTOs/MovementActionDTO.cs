using UnityEngine;

namespace An01malia.FirstPerson.PlayerModule.States.DTOs
{
    public class MovementActionDTO : ActionDTO
    {
        #region Properties

        public Vector3 Movement { get; }

        #endregion

        #region Constructor

        public MovementActionDTO(Vector3 movement)
        {
            Movement = movement;
        }

        #endregion
    }
}
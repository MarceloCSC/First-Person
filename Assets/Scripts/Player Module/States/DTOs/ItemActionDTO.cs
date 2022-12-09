using UnityEngine;

namespace An01malia.FirstPerson.PlayerModule.States.DTOs
{
    public class ItemActionDTO : ActionDTO
    {
        #region Properties

        public Transform Item { get; }

        #endregion

        #region Constructor

        public ItemActionDTO(Transform item)
        {
            Item = item;
        }

        #endregion
    }
}
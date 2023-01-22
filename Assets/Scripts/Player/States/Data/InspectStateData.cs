using An01malia.FirstPerson.ItemModule.Items;
using An01malia.FirstPerson.PlayerModule.States.DTOs;
using UnityEngine;

namespace An01malia.FirstPerson.PlayerModule.States.Data
{
    public class InspectStateData : PlayerStateData
    {
        #region Properties

        public ItemToInspect ItemToInspect;
        public GameObject Prefab;

        #endregion

        #region Constructor

        public InspectStateData(PlayerActionDTO dto) : base(dto)
        {
        }

        #endregion
    }
}
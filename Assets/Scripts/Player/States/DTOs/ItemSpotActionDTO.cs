using An01malia.FirstPerson.InteractionModule.Environment;

namespace An01malia.FirstPerson.PlayerModule.States.DTOs
{
    public class ItemSpotActionDTO : ActionDTO
    {
        #region Properties

        public ItemSpot ItemSpot { get; }

        #endregion

        #region Constructor

        public ItemSpotActionDTO(ItemSpot itemSpot)
        {
            ItemSpot = itemSpot;
        }

        #endregion
    }
}
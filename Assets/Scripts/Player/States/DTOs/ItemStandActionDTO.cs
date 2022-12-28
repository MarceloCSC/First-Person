using An01malia.FirstPerson.ItemModule;

namespace An01malia.FirstPerson.PlayerModule.States.DTOs
{
    public class ItemStandActionDTO : ActionDTO
    {
        #region Properties

        public ItemStand ItemStand { get; }

        #endregion

        #region Constructor

        public ItemStandActionDTO(ItemStand itemStand)
        {
            ItemStand = itemStand;
        }

        #endregion
    }
}
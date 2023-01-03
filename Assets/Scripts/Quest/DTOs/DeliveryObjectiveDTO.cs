namespace An01malia.FirstPerson.QuestModule.DTOs
{
    public class DeliveryObjectiveDTO : ObjectiveDTO
    {
        #region Fields

        private readonly string _itemId;
        private readonly string _recipientId;

        #endregion

        #region Properties

        public string ItemId => _itemId;
        public string RecipientId => _recipientId;

        #endregion

        #region Constructor

        public DeliveryObjectiveDTO(string itemId, string recipientId)
        {
            _itemId = itemId;
            _recipientId = recipientId;
        }

        #endregion
    }
}
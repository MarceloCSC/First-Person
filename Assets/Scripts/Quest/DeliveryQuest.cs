namespace An01malia.FirstPerson.QuestModule
{
    public class DeliveryQuest : IQuest
    {
        #region Fields

        private readonly QuestRecipient _recipient;

        #endregion

        #region Properties

        public IQuestGoal QuestGoal { get => _recipient; }

        public bool SatisfyConditions => throw new System.NotImplementedException();

        #endregion

        #region Constructor

        public DeliveryQuest(QuestRecipient recipient)
        {
            _recipient = recipient;
        }

        #endregion
    }
}
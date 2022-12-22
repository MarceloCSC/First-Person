namespace An01malia.FirstPerson.QuestModule
{
    public interface IQuest
    {
        IQuestGoal QuestGoal { get; }
        bool SatisfyConditions { get; }
    }
}
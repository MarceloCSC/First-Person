using An01malia.FirstPerson.QuestModule.DTOs;
using System;

namespace An01malia.FirstPerson.QuestModule
{
    public interface IQuest
    {
        string Title { get; }
        string Id { get; }
        string Description { get; }
        bool IsCompleted { get; }
        DateTime StartedOn { get; }
        DateTime UpdatedOn { get; }
        DateTime CompletedOn { get; }

        void Initialize();

        bool SatisfyConditions();

        bool TryCompleteObjective(ObjectiveDTO dto);

        bool Contains(ObjectiveDTO dto);
    }
}
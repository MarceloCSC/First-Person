using An01malia.FirstPerson.QuestModule.DTOs;
using System;

namespace An01malia.FirstPerson.QuestModule
{
    public interface IQuest
    {
        string Title { get; }
        string Id { get; }
        string Description { get; }
        DateTime StartedOn { get; }
        DateTime UpdatedOn { get; }
        DateTime CompletedOn { get; }

        bool SatisfyConditions();

        bool TryCompleteObjective(ObjectiveDTO objectiveDto);

        bool Contains(ObjectiveDTO objectiveDto);
    }
}
using An01malia.FirstPerson.QuestModule.DTOs;
using System;

namespace An01malia.FirstPerson.QuestModule
{
    public interface IQuestObjective
    {
        bool IsCompleted { get; }
        bool IsSuccessful { get; }
        DateTime CompletedOn { get; }

        void Complete(bool isSuccessful);

        bool HasValues(ObjectiveDTO dto);
    }
}
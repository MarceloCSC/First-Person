using An01malia.FirstPerson.Core;
using System.Collections.Generic;

namespace An01malia.FirstPerson.QuestModule
{
    public class QuestManager : ComponentSingleton<QuestManager>
    {
        private ICollection<IQuest> _activeQuests;
        private ICollection<IQuest> _completedQuests;

        protected override void Initialize()
        {
        }

        public void CompleteQuest(IQuest quest)
        {
            if (quest.SatisfyConditions && _activeQuests.Contains(quest))
            {
                _activeQuests.Remove(quest);
                _completedQuests.Add(quest);
            }
        }
    }
}
using An01malia.FirstPerson.Core;
using An01malia.FirstPerson.QuestModule.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace An01malia.FirstPerson.QuestModule
{
    public class QuestManager : ComponentSingleton<QuestManager>
    {
        #region Fields

        private ICollection<IQuest> _activeQuests;
        private ICollection<IQuest> _completedQuests;

        #endregion

        #region Public Methods

        public void StartQuest(IQuest quest)
        {
            quest.Initialize();

            _activeQuests.Add(quest);
        }

        public bool TryCompleteObjective(ObjectiveDTO objectiveDto)
        {
            var quest = _activeQuests.Where(quest => quest.Contains(objectiveDto)).FirstOrDefault();

            if (quest == null) return false;

            if (!quest.TryCompleteObjective(objectiveDto)) return false;

            TryCompleteQuest(quest);

            return true;
        }

        public void EndQuest(string questId)
        {
            var quest = _activeQuests.Where(quest => quest.Id == questId).FirstOrDefault();

            if (quest == null) return;

            quest.End();

            _activeQuests.Remove(quest);
            _completedQuests.Add(quest);
        }

        #endregion

        #region Protected Methods

        protected override void Initialize()
        {
            _activeQuests = new List<IQuest>();
            _completedQuests = new List<IQuest>();
        }

        #endregion

        #region Private Methods

        private bool TryCompleteQuest(IQuest quest)
        {
            if (quest == null) return false;

            if (!quest.SatisfyConditions()) return false;

            _activeQuests.Remove(quest);
            _completedQuests.Add(quest);

            return true;
        }

        #endregion
    }
}
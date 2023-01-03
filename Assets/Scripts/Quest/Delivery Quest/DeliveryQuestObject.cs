using An01malia.FirstPerson.QuestModule.DTOs;
using System;
using System.Linq;
using UnityEngine;

namespace An01malia.FirstPerson.QuestModule.DeliveryQuest
{
    [CreateAssetMenu(fileName = "NewDeliveryQuest", menuName = "Quests/New Delivery Quest")]
    public class DeliveryQuestObject : QuestObject
    {
        #region Fields

        [Space]
        [SerializeField] private DeliveryQuestObjective[] _objectives;

        #endregion

        #region Unity Methods

        private void OnEnable()
        {
            if (_objectives == null) return;

            if (_objectives.Length == 0) Debug.LogWarning($"There are no objectives assigned to Quest Object '{name}'.");
        }

        #endregion

        #region Public Methods

        public override bool SatisfyConditions()
        {
            IsCompleted = _objectives.All(objective => objective.IsCompleted);

            if (IsCompleted) CompletedOn = DateTime.UtcNow;

            return IsCompleted;
        }

        public override bool TryCompleteObjective(ObjectiveDTO dto)
        {
            if (dto is not DeliveryObjectiveDTO objectiveDto) return false;

            var objective = _objectives.Where(objective => objective.HasValues(objectiveDto)).FirstOrDefault();

            if (objective == null) return false;

            objective.Complete(true);

            UpdatedOn = DateTime.UtcNow;

            return true;
        }

        public override bool Contains(ObjectiveDTO dto)
        {
            return _objectives.Any(objective => objective.HasValues(dto));
        }

        #endregion
    }
}
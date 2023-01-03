using An01malia.FirstPerson.QuestModule.DTOs;
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

        private void OnEnable()
        {
            if (_objectives == null) return;

            if (_objectives.Length == 0) Debug.LogWarning($"There are no objectives assigned to Quest Object '{name}'.");
        }

        #region Public Methods

        public override bool SatisfyConditions() => _objectives.All(objective => objective.IsCompleted);

        public override bool TryCompleteObjective(ObjectiveDTO objectiveDto)
        {
            if (objectiveDto is DeliveryObjectiveDTO deliveryDto)
            {
                foreach (var objective in _objectives)
                {
                    if (objective.ItemId == deliveryDto.ItemId &&
                        objective.RecipientId == deliveryDto.RecipientId)
                    {
                        objective.Complete(true);

                        return true;
                    }
                }
            }

            return false;
        }

        public override bool Contains(ObjectiveDTO objectiveDto)
        {
            return objectiveDto is DeliveryObjectiveDTO deliveryDto &&
                   _objectives.Any(objective => objective.ItemId == deliveryDto.ItemId &&
                                                objective.RecipientId == deliveryDto.RecipientId);
        }

        #endregion
    }
}
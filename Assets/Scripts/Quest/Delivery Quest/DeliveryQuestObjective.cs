using An01malia.FirstPerson.ItemModule;
using An01malia.FirstPerson.NonPlayableCharacterModule;
using An01malia.FirstPerson.QuestModule.DTOs;
using System;
using UnityEngine;

namespace An01malia.FirstPerson.QuestModule.DeliveryQuest
{
    [Serializable]
    public class DeliveryQuestObjective : IQuestObjective
    {
        #region Fields

        [SerializeField] private ItemObject _item;
        [SerializeField] private CharacterObject _recipient;

        [Space]
        [SerializeField] private bool _isCompleted;
        [SerializeField] private bool _isSuccessful;

        private DateTime _completedOn;

        #endregion

        #region Properties

        public string ItemId => _item.Id;
        public string RecipientId => _recipient.Id;
        public bool IsCompleted => _isCompleted;
        public bool IsSuccessful => _isSuccessful;
        public DateTime CompletedOn => _completedOn;

        #endregion

        #region Public Methods

        public void Complete(bool isSuccessful)
        {
            _isCompleted = true;
            _isSuccessful = isSuccessful;
            _completedOn = DateTime.UtcNow;
        }

        public bool HasValues(ObjectiveDTO dto)
        {
            return dto is DeliveryObjectiveDTO objectiveDto &&
                   ItemId == objectiveDto.ItemId && RecipientId == objectiveDto.RecipientId;
        }

        #endregion
    }
}
using An01malia.FirstPerson.ItemModule.Items;
using An01malia.FirstPerson.NonPlayableCharacterModule;
using An01malia.FirstPerson.QuestModule;
using An01malia.FirstPerson.QuestModule.DTOs;
using UnityEngine;

namespace An01malia.FirstPerson.InteractionModule.Environment
{
    public class QuestItemSpot : ItemSpot
    {
        #region Fields

        [SerializeField] private CharacterObject _character;

        #endregion

        #region Unity Methods

        private void Start()
        {
            if (!_character) Debug.LogWarning($"Missing 'CharacterObject' in {name} game object.");
        }

        #endregion

        #region Public Methods

        public override bool TryPlaceItem(Transform transform)
        {
            if (!CanPlaceItem(transform, out ItemToCarry item)) return false;

            bool isCompleted = QuestManager.Instance.TryCompleteObjective(new DeliveryObjectiveDTO(item.Root.Id, _character.Id));

            if (isCompleted) Place(item);

            return isCompleted;
        }

        #endregion
    }
}
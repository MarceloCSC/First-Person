using An01malia.FirstPerson.QuestModule.DTOs;
using System;
using UnityEditor;
using UnityEngine;

namespace An01malia.FirstPerson.QuestModule
{
    public abstract class QuestObject : ScriptableObject, IQuest
    {
        #region Fields

        [SerializeField] private string _questTitle;

        [Space]
        [SerializeField] private string _uniqueId;

        [Space]
        [TextArea(10, 30)]
        [SerializeField] private string _description;

        private DateTime _startedOn;
        private DateTime _updatedOn;
        private DateTime _completedOn;

        #endregion

        #region Properties

        public string Title => _questTitle;
        public string Id => _uniqueId;
        public string Description => _description;
        public DateTime StartedOn => _startedOn;
        public DateTime UpdatedOn => _updatedOn;
        public DateTime CompletedOn => _completedOn;

        #endregion

        #region Unity Methods

        private void OnValidate()
        {
            if (!string.IsNullOrEmpty(_uniqueId)) return;

            string path = AssetDatabase.GetAssetPath(this);

            _uniqueId = AssetDatabase.AssetPathToGUID(path);
        }

        #endregion

        #region Public Methods

        public abstract bool SatisfyConditions();

        public abstract bool TryCompleteObjective(ObjectiveDTO objectiveDto);

        public abstract bool Contains(ObjectiveDTO objectiveDto);

        #endregion
    }
}
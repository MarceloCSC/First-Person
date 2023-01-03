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

        [Space]
        [SerializeField] private bool _isCompleted;

        #endregion

        #region Properties

        public string Title => _questTitle;
        public string Id => _uniqueId;
        public string Description => _description;
        public bool IsCompleted { get => _isCompleted; protected set => _isCompleted = value; }
        public DateTime StartedOn { get; protected set; }
        public DateTime UpdatedOn { get; protected set; }
        public DateTime CompletedOn { get; protected set; }

        #endregion

        #region Unity Methods

        private void OnValidate()
        {
            if (!string.IsNullOrEmpty(_uniqueId)) return;

            string path = AssetDatabase.GetAssetPath(this);

            _uniqueId = AssetDatabase.AssetPathToGUID(path);
        }

        #endregion

        #region Abstract Methods

        public abstract bool SatisfyConditions();

        public abstract bool TryCompleteObjective(ObjectiveDTO dto);

        public abstract bool Contains(ObjectiveDTO dto);

        #endregion

        #region Public Methods

        public virtual void Initialize()
        {
            StartedOn = DateTime.UtcNow;
        }

        #endregion
    }
}
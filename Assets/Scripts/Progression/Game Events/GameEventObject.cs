using An01malia.FirstPerson.QuestModule;
using System;
using UnityEditor;
using UnityEngine;

namespace An01malia.FirstPerson.ProgressionModule.GameEvents
{
    public abstract class GameEventObject : ScriptableObject
    {
        #region Fields

        [SerializeField] private string _title;

        [Space]
        [SerializeField] private string _uniqueId;

        [Space]
        [TextArea(10, 30)]
        [SerializeField] private string _description;

        [Space]
        [SerializeField] private QuestObject _correspondingQuest;

        [Space]
        [SerializeField] private string _variable;

        #endregion

        #region Properties

        public string Title => _title;
        public string Id => _uniqueId;
        public string Description => _description;
        public string VariableName => _variable;
        public abstract object VariableValue { get; }
        public QuestObject CorrespondingQuest => _correspondingQuest;
        public DateTime ObtainedOn { get; protected set; }

        #endregion

        #region Unity Methods

        private void OnValidate()
        {
            if (!string.IsNullOrEmpty(_uniqueId)) return;

            string path = AssetDatabase.GetAssetPath(this);

            _uniqueId = AssetDatabase.AssetPathToGUID(path);
        }

        #endregion
    }
}
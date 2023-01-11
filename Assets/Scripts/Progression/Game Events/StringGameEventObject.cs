using UnityEngine;

namespace An01malia.FirstPerson.ProgressionModule.GameEvents
{
    [CreateAssetMenu(fileName = "NewGameEvent", menuName = "Progression/New String Game Event")]
    public class StringGameEventObject : GameEventObject
    {
        #region Fields

        [SerializeField] private string _value;

        #endregion

        #region Properties

        public override object VariableValue => _value;

        #endregion
    }
}
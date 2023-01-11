using UnityEngine;

namespace An01malia.FirstPerson.ProgressionModule.GameEvents
{
    [CreateAssetMenu(fileName = "NewGameEvent", menuName = "Progression/New Float Game Event")]
    public class FloatGameEventObject : GameEventObject
    {
        #region Fields

        [SerializeField] private float _value;

        #endregion

        #region Properties

        public override object VariableValue => _value;

        #endregion
    }
}
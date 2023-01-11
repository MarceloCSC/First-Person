using UnityEngine;

namespace An01malia.FirstPerson.ProgressionModule.GameEvents
{
    [CreateAssetMenu(fileName = "NewGameEvent", menuName = "Progression/New Boolean Game Event")]
    public class BooleanGameEventObject : GameEventObject
    {
        #region Fields

        [SerializeField] private bool _value;

        #endregion

        #region Properties

        public override object VariableValue => _value;

        #endregion
    }
}
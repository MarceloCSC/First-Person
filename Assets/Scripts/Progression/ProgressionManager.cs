using An01malia.FirstPerson.Core;
using An01malia.FirstPerson.ProgressionModule.GameEvents;
using System.Collections.Generic;
using UnityEngine;

namespace An01malia.FirstPerson.ProgressionModule
{
    public class ProgressionManager : ComponentSingleton<ProgressionManager>
    {
        #region Fields

        [SerializeField] private List<GameEventObject> _allEvents;

        private ICollection<GameEventObject> _occurredEvents;

        #endregion

        #region Properties

        public List<GameEventObject> AllEvents => _allEvents;

        #endregion

        #region Protected Methods

        protected override void Initialize()
        {
            _occurredEvents = new List<GameEventObject>();
        }

        #endregion
    }
}
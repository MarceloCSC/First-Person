using UnityEngine;

namespace An01malia.FirstPerson.Interaction
{
    public class Climbable : MonoBehaviour
    {
        #region Fields

        [SerializeField] private bool _canClimbUpwards = true;
        [SerializeField] private bool _canClimbSideways = false;

        #endregion Fields

        #region Properties

        public bool CanClimbUpwards => _canClimbUpwards;
        public bool CanClimbSideways => _canClimbSideways;

        #endregion Properties
    }
}
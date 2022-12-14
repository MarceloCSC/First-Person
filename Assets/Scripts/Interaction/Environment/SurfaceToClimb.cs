using UnityEngine;

namespace An01malia.FirstPerson.InteractionModule.Environment
{
    public class SurfaceToClimb : MonoBehaviour
    {
        #region Fields

        [SerializeField] private bool _canClimbUpwards = true;
        [SerializeField] private bool _canClimbSideways;

        #endregion Fields

        #region Properties

        public bool CanClimbUpwards => _canClimbUpwards;
        public bool CanClimbSideways => _canClimbSideways;

        #endregion Properties
    }
}
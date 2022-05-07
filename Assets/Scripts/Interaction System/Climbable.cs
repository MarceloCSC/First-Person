using UnityEngine;

namespace FirstPerson.Interaction
{
    // First implemented by Marcelo de Souza Camargo

    public class Climbable : MonoBehaviour
    {
        #region Fields

        [SerializeField] private bool _canClimbUpwards = true;
        [SerializeField] private bool _canClimbSideways = false;

        #endregion

        #region Properties

        public bool CanClimbUpwards => _canClimbUpwards;
        public bool CanClimbSideways => _canClimbSideways;

        #endregion
    }
}
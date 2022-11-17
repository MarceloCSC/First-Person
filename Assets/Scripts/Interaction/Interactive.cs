using UnityEngine;

namespace An01malia.FirstPerson.Interaction
{
    public class Interactive : MonoBehaviour, IInteractive
    {
        #region Fields

        [SerializeField] protected bool _isActivated = false;
        [SerializeField] protected bool _isLocked = true;

        #endregion

        #region Properties

        public bool IsLocked { get => _isLocked; set => _isLocked = value; }

        #endregion

        #region Public Methods

        public virtual void StartInteraction()
        {
            if (!_isLocked)
            {
                _isActivated = !_isActivated;
            }
        }

        #endregion
    }
}
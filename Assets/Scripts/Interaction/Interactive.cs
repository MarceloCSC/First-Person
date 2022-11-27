using UnityEngine;

namespace An01malia.FirstPerson.InteractionModule
{
    public class Interactive : MonoBehaviour, IInteractive
    {
        #region Fields

        [SerializeField] private bool _isLocked = true;
        [SerializeField] protected bool IsActivated;

        #endregion

        #region Properties

        public bool IsLocked { get => _isLocked; set => _isLocked = value; }

        #endregion

        #region Public Methods

        public virtual void StartInteraction()
        {
            if (!_isLocked)
            {
                IsActivated = !IsActivated;
            }
        }

        #endregion
    }
}
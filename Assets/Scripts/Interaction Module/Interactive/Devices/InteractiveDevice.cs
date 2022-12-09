using UnityEngine;

namespace An01malia.FirstPerson.InteractionModule.Interactive.Devices
{
    public class InteractiveDevice : MonoBehaviour, IInteractive
    {
        #region Fields

        [SerializeField] private bool _canInteract = true;
        [SerializeField] protected bool IsActivated;

        #endregion

        #region Properties

        public bool CanInteract { get => _canInteract; set => _canInteract = value; }

        #endregion

        #region Public Methods

        public virtual void StartInteraction()
        {
            if (!_canInteract) return;

            IsActivated = !IsActivated;
        }

        #endregion
    }
}
using An01malia.FirstPerson.DialogueModule;
using UnityEngine;

namespace An01malia.FirstPerson.InteractionModule.Interactive
{
    public class NPC : MonoBehaviour, IInteractive
    {
        #region Fields

        [SerializeField] private TextAsset _dialogue;
        [SerializeField] private bool _canInteract = true;

        #endregion

        #region Properties

        public bool CanInteract { get => _canInteract; set => _canInteract = value; }

        #endregion

        #region Public Methods

        public virtual void StartInteraction()
        {
            if (!_canInteract) return;

            DialogueManager.Instance.EnterDialogue(_dialogue);
        }

        #endregion
    }
}
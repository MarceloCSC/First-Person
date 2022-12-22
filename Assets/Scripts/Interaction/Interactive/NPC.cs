using An01malia.FirstPerson.DialogueModule;
using An01malia.FirstPerson.DialogueModule.DTOs;
using An01malia.FirstPerson.NonPlayableCharacterModule;
using UnityEngine;

namespace An01malia.FirstPerson.InteractionModule.Interactive
{
    public class NPC : MonoBehaviour, IInteractive
    {
        #region Fields

        [SerializeField] private CharacterObject _character;
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

            DialogueManager.Instance.StartDialogue(new DialogueDTO(_dialogue.text, _character.Name));
        }

        #endregion
    }
}
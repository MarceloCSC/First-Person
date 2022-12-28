using An01malia.FirstPerson.Core;
using An01malia.FirstPerson.DialogueModule.DTOs;
using An01malia.FirstPerson.UserInterfaceModule;
using Ink.Runtime;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace An01malia.FirstPerson.DialogueModule
{
    public class DialogueManager : ComponentSingleton<DialogueManager>
    {
        #region Delegates

        public event Action<DialogueDTO> OnDialogueContinued = delegate { };

        public event Action<DialogueDTO> OnDialogueSkipped = delegate { };

        public event Action<ICollection<Choice>> OnChoicesDisplayed = delegate { };

        public event Action OnDialogueEnded = delegate { };

        #endregion

        #region Fields

        [SerializeField] private TextAsset _globalVariables;

        public static GameObject Panel;

        private bool _isTyping;
        private Story _currentStory;
        private DialogueVariablesHandler _variablesHandler;

        #endregion

        #region Properties

        public bool CanQuitDialogue => _currentStory == null;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            _variablesHandler = new DialogueVariablesHandler(_globalVariables.text);
        }

        #endregion

        #region Public Methods

        public void StartDialogue(DialogueDTO dto)
        {
            GameStateManager.Instance.ChangeState(GameState.UI);

            UIPanelManager.ToggleUIPanel(Panel);

            _currentStory = new Story(dto.Dialogue);
            _currentStory.EvaluateFunction("setSpeakerName", dto.Speaker);

            _variablesHandler.StartListening(_currentStory);

            TryContinueDialogue();
        }

        public void HandleDialogue()
        {
            if (ShouldSkipDialogue()) return;

            var selectedIndex = GetSelectedIndex();

            if (_currentStory.currentChoices.Count > 0 && selectedIndex >= 0)
            {
                Choose(selectedIndex);
            }

            Continue();
        }

        public void Choose(int index)
        {
            _currentStory.ChooseChoiceIndex(index);
        }

        public void Continue()
        {
            bool canContinue = TryContinueDialogue();

            if (!canContinue) EndDialogue();
        }

        public void EndTyping() => _isTyping = false;

        #endregion

        #region Protected Methods

        protected override void Initialize()
        {
        }

        #endregion

        #region Private Methods

        private bool TryContinueDialogue()
        {
            bool canContinue = _currentStory.canContinue;

            if (canContinue)
            {
                _isTyping = true;

                string dialogue = _currentStory.Continue();

                var dto = new DialogueDTO(dialogue, "Placeholder", _currentStory.canContinue);

                OnDialogueContinued(dto);
                OnChoicesDisplayed(_currentStory.currentChoices);
            }

            return canContinue;
        }

        private void EndDialogue()
        {
            GameStateManager.Instance.ChangeState(GameState.Gameplay);

            UIPanelManager.ToggleUIPanel(Panel);

            _variablesHandler.StopListening(_currentStory);
            _currentStory = null;

            OnDialogueEnded();
        }

        private int GetSelectedIndex()
        {
            return EventSystem.current.currentSelectedGameObject ?
                   EventSystem.current.currentSelectedGameObject.transform.GetSiblingIndex() :
                   -1;
        }

        private bool ShouldSkipDialogue()
        {
            if (!_isTyping) return false;

            var dto = new DialogueDTO(_currentStory.currentText, string.Empty, _currentStory.canContinue);

            OnDialogueSkipped(dto);

            return true;
        }

        #endregion
    }
}
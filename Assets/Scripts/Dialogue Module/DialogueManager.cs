using An01malia.FirstPerson.Core;
using An01malia.FirstPerson.UIModule;
using Ink.Runtime;
using TMPro;
using UnityEngine;

namespace An01malia.FirstPerson.DialogueModule
{
    public class DialogueManager : Singleton<DialogueManager>
    {
        #region Fields

        [SerializeField] private GameObject _choicePrefab;

        public static GameObject Panel;

        private Story _currentStory;
        private TextMeshProUGUI _textGui;

        #endregion

        #region Properties

        public bool CanContinue => _currentStory != null;

        #endregion

        #region Public Methods

        public void EnterDialogue(TextAsset json)
        {
            GameStateManager.Instance.ChangeState(GameState.UI);

            UIPanelManager.ToggleUIPanel(Panel);

            _currentStory = new Story(json.text);

            ContinueDialogue();
        }

        public void ContinueDialogue()
        {
            if (_currentStory.canContinue)
            {
                _textGui.text = _currentStory.Continue();
            }
            else
            {
                ExitDialogue();
            }
        }

        #endregion

        #region Protected Methods

        protected override void Initialize()
        {
            _textGui = Panel.GetComponentInChildren<TextMeshProUGUI>();
        }

        #endregion

        #region Private Methods

        private void ExitDialogue()
        {
            GameStateManager.Instance.ChangeState(GameState.Gameplay);

            UIPanelManager.ToggleUIPanel(Panel);

            _currentStory = null;
            _textGui.text = string.Empty;
        }

        #endregion
    }
}
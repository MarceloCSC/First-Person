using An01malia.FirstPerson.DialogueModule;
using An01malia.FirstPerson.DialogueModule.DTOs;
using Ink.Runtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace An01malia.FirstPerson.UserInterfaceModule.Dialogue
{
    public class UIDialogueDisplay : MonoBehaviour
    {
        #region Fields

        [SerializeField] private TextMeshProUGUI _dialogueText;
        [SerializeField] private TextMeshProUGUI _speakerText;
        [SerializeField] private Button _nextButton;
        [SerializeField] private GameObject _choicePrefab;
        [SerializeField] private Transform _choicesGrid;
        [SerializeField] private float _typingSpeed = 0.03f;

        private Coroutine _typingCoroutine;

        #endregion

        #region Unity Methods

        private void OnEnable()
        {
            DialogueManager.Instance.OnDialogueContinued += HandleDialogue;
            DialogueManager.Instance.OnChoicesDisplayed += HandleChoices;
            DialogueManager.Instance.OnDialogueSkipped += HandleSkip;
            DialogueManager.Instance.OnDialogueEnded += ClearChoices;

            _nextButton.onClick.AddListener(DialogueManager.Instance.Continue);
        }

        private void OnDisable()
        {
            if (DialogueManager.Instance == null) return;

            DialogueManager.Instance.OnDialogueContinued -= HandleDialogue;
            DialogueManager.Instance.OnChoicesDisplayed -= HandleChoices;
            DialogueManager.Instance.OnDialogueSkipped -= HandleSkip;
            DialogueManager.Instance.OnDialogueEnded -= ClearChoices;

            _nextButton.onClick.RemoveListener(DialogueManager.Instance.Continue);
        }

        #endregion

        #region Private Methods

        private void HandleDialogue(DialogueDTO dto)
        {
            if (_typingCoroutine != null) StopCoroutine(_typingCoroutine);

            _nextButton.gameObject.SetActive(false);
            _typingCoroutine = StartCoroutine(DisplayLine(dto));
        }

        private void HandleChoices(ICollection<Choice> currentChoices)
        {
            if (currentChoices == null || currentChoices.Count == 0)
            {
                ClearChoices();

                return;
            }

            DisplayChoices(currentChoices);
        }

        private void DisplayChoices(ICollection<Choice> currentChoices)
        {
            foreach (var choice in currentChoices)
            {
                var gameObject = Instantiate(_choicePrefab, _choicesGrid);
                gameObject.GetComponentInChildren<TextMeshProUGUI>().text = choice.text;
            }

            EventSystem.current.SetSelectedGameObject(_choicesGrid.GetChild(0).gameObject);
        }

        private void ClearChoices()
        {
            if (_choicesGrid.childCount == 0) return;

            foreach (Transform child in _choicesGrid)
            {
                Destroy(child.gameObject);
            }
        }

        private IEnumerator DisplayLine(DialogueDTO dto)
        {
            _dialogueText.text = dto.Dialogue;
            _dialogueText.maxVisibleCharacters = 0;
            _speakerText.text = dto.Speaker != string.Empty ? dto.Speaker : _speakerText.text;

            for (int i = 0; i < dto.Dialogue.Length; i++)
            {
                _dialogueText.maxVisibleCharacters++;

                yield return new WaitForSeconds(_typingSpeed);
            }

            _nextButton.gameObject.SetActive(dto.CanContinue);

            DialogueManager.Instance.EndTyping();

            yield return null;
        }

        private void HandleSkip(DialogueDTO dto)
        {
            StopCoroutine(_typingCoroutine);

            _dialogueText.maxVisibleCharacters = dto.Dialogue.Length;
            _nextButton.gameObject.SetActive(dto.CanContinue);

            DialogueManager.Instance.EndTyping();
        }

        #endregion
    }
}
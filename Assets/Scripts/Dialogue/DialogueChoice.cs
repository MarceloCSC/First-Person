using UnityEngine;
using UnityEngine.UI;

namespace An01malia.FirstPerson.DialogueModule
{
    public class DialogueChoice : MonoBehaviour
    {
        #region Unity Methods

        private void OnEnable()
        {
            GetComponent<Button>().onClick.AddListener(OnClick);
        }

        private void OnDisable()
        {
            GetComponent<Button>().onClick.RemoveListener(OnClick);
        }

        #endregion

        #region Private Methods

        private void OnClick()
        {
            DialogueManager.Instance.Choose(transform.GetSiblingIndex());
            DialogueManager.Instance.Continue();
        }

        #endregion
    }
}
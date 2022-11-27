using UnityEngine;
using UnityEngine.UI;

namespace An01malia.FirstPerson.UIModule
{
    public class UIInteraction : MonoBehaviour
    {
        #region Fields

        [SerializeField] private Text _interactionDisplay;

        [Header("Verbs to Display")]
        [SerializeField] private string _open = "Open ";
        [SerializeField] private string _pickUp = "Pick up ";
        [SerializeField] private string _use = "Use ";

        #endregion

        #region Unity Methods

        private void Awake()
        {
            SetReferences();
        }

        //private void OnEnable()
        //{
        //    playerInteraction.OnRayCast += DisplayObject;
        //}

        //private void OnDisable()
        //{
        //    playerInteraction.OnRayCast -= DisplayObject;
        //}

        #endregion

        #region Private Methods

        private void DisplayObject(bool isInteractable)
        {
            _interactionDisplay.enabled = isInteractable;
        }

        private void SetReferences()
        {
            //playerInteraction = References.Player.GetComponent<PlayerInteraction>();
        }

        #endregion
    }
}
using UnityEngine;
using UnityEngine.UI;

namespace An01malia.FirstPerson.UI
{
    public class UIInteraction : MonoBehaviour
    {
        [SerializeField] private Text interactionDisplay = null;

        [Header("Verbs to Display")]
        [SerializeField] private string open = "Open ";
        [SerializeField] private string pickUp = "Pick up ";
        [SerializeField] private string use = "Use ";

        private void Awake()
        {
            SetReferences();
        }

        //private void OnEnable()
        //{
        //    playerInteraction.OnRayCast += DisplayObject;
        //}

        private void DisplayObject(bool isInteractable)
        {
            interactionDisplay.enabled = isInteractable;
        }

        //private void OnDisable()
        //{
        //    playerInteraction.OnRayCast -= DisplayObject;
        //}

        private void SetReferences()
        {
            //playerInteraction = References.Player.GetComponent<PlayerInteraction>();
        }
    }
}
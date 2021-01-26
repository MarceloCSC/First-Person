using System.Text;
using UnityEngine;
using UnityEngine.UI;
using FirstPerson.Interaction;

namespace FirstPerson.UI
{

    public class UIInteraction : MonoBehaviour
    {

        [SerializeField] Text interactionDisplay = null;

        [Header("Verbs to Display")]
        [SerializeField] string open = "Open ";
        [SerializeField] string pickUp = "Pick up ";
        [SerializeField] string use = "Use ";


        #region Cached references
        private PlayerInteraction playerInteraction;
        #endregion


        private void Awake()
        {
            SetReferences();
        }

        private void OnEnable()
        {
            playerInteraction.OnRayCast += DisplayObject;
        }

        private void DisplayObject(bool isInteractable)
        {
            interactionDisplay.enabled = isInteractable;

            if (isInteractable == true)
            {
                interactionDisplay.text = GetDisplayText(PlayerInteraction.InteractableObject);
            }
        }

        private string GetDisplayText(Interactable interactable)
        {
            StringBuilder stringBuilder = new StringBuilder();

            if (interactable is PickUp)
            {
                stringBuilder.Append(pickUp);
            }
            else if (interactable is StorageUnit)
            {
                stringBuilder.Append(open);
            }
            else
            {
                stringBuilder.Append(use);
            }

            stringBuilder.Append(interactable.Name);

            return stringBuilder.ToString();
        }

        private void OnDisable()
        {
            playerInteraction.OnRayCast -= DisplayObject;
        }

        private void SetReferences()
        {
            playerInteraction = References.Player.GetComponent<PlayerInteraction>();
        }

    }

}

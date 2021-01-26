using UnityEngine;

namespace FirstPerson.Interaction
{

    public class Interactable : MonoBehaviour
    {

        [SerializeField] string nameToDisplay = null;


        #region Properties
        public virtual string Name => nameToDisplay;
        #endregion


        #region Cached references
        protected PlayerInteraction player;
        #endregion


        private void Awake()
        {
            SetReferences();
        }

        protected virtual void InteractWith()
        {
        }

        protected virtual void TakeAll()
        {
        }

        protected virtual void OutOfRange()
        {
        }

        protected virtual bool IsSame()
        {
            return PlayerInteraction.InteractableObject != null
                && PlayerInteraction.InteractableObject == this;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject == References.Player)
            {
                player.OnInteraction += InteractWith;
                player.OnButtonHeldDown += TakeAll;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject == References.Player)
            {
                OutOfRange();
                player.OnInteraction -= InteractWith;
                player.OnButtonHeldDown -= TakeAll;
            }
        }

        private void OnDisable()
        {
            player.OnInteraction -= InteractWith;
            player.OnButtonHeldDown -= TakeAll;
        }

        protected virtual void SetReferences()
        {
            player = References.Player.GetComponent<PlayerInteraction>();
        }

    }

}

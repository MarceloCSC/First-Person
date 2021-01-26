using System;
using UnityEngine;

namespace FirstPerson.Interaction
{

    public class PlayerInteraction : MonoBehaviour
    {

        public event Action OnInteraction = delegate { };
        public event Action OnButtonHeldDown = delegate { };
        public event Action<bool> OnRayCast = delegate { };


        [SerializeField] float rayLength = 2.0f;
        [SerializeField] LayerMask interactableLayers = default;

        [Space]
        [SerializeField] bool toggleGizmos = false;

        private static Interactable interactableObject;

        private float delay = 1.0f;
        private float buttonTimer = 0.0f;


        #region Properties
        public static Interactable InteractableObject
        {
            get => interactableObject;
            set
            {
                if (value == interactableObject) { return; }

                interactableObject = value;
            }
        }
        #endregion


        private void Update()
        {
            DetectRaycast();

            if (Input.GetButtonDown(Control.Interact))
            {
                buttonTimer = Time.time + delay;
                OnInteraction();
            }

            if (Input.GetButton(Control.Interact) && Time.time >= buttonTimer)
            {
                OnButtonHeldDown();
            }
        }

        private void DetectRaycast()
        {
            Ray ray = new Ray(References.PlayerSight.position, References.PlayerSight.forward);

            if (Physics.Raycast(ray, out RaycastHit hit, rayLength, interactableLayers, QueryTriggerInteraction.Ignore))
            {
                InteractableObject = hit.transform.GetComponent<Interactable>();
                OnRayCast(true);
            }
            else
            {
                InteractableObject = null;
                OnRayCast(false);
            }
        }

        private void OnDrawGizmos()
        {
            if (toggleGizmos && Application.isPlaying)
            {
                Gizmos.color = Color.cyan;
                Gizmos.DrawRay(References.PlayerSight.position, References.PlayerSight.forward * rayLength);
            }
        }

    }

}

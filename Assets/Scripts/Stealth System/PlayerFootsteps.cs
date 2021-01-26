using System.Collections;
using UnityEngine;
using FirstPerson.PlayerController;

namespace FirstPerson.Stealth
{

    public class PlayerFootsteps : Noise
    {

        [SerializeField] float runningDistance = 5.0f;

        [Header("Footsteps Interval")]
        [SerializeField] float defaultInterval = 0.5f;
        [SerializeField] float runningInterval = 0.2f;

        [Space]
        [SerializeField] float coroutineInterval = 0.1f;

        private Coroutine footsteps;


        #region Cached references
        private PlayerMovement player;
        private CharacterController character;
        #endregion


        protected override void OnEnable()
        {
        }

        private void Start()
        {
            hearingDistance = defaultDistance;
            StartCoroutine(HandleFootsteps());
        }

        private IEnumerator HandleFootsteps()
        {
            while (gameObject.activeSelf)
            {
                if (character.velocity.sqrMagnitude != 0.0f && footsteps == null)
                {
                    footsteps = StartCoroutine(FootstepsNoise());
                }
                else if (character.velocity.sqrMagnitude < 0.1f && footsteps != null)
                {
                    StopCoroutine(footsteps);
                    footsteps = null;
                }

                yield return new WaitForSeconds(coroutineInterval);
            }
        }

        private IEnumerator FootstepsNoise()
        {
            while (character.velocity.sqrMagnitude != 0.0f)
            {
                yield return new WaitForSeconds(defaultInterval);

                MakeNoise();
            }
        }

        protected override void SetReferences()
        {
            base.SetReferences();
            player = GetComponentInParent<PlayerMovement>();
            character = GetComponentInParent<CharacterController>();
        }

    }

}

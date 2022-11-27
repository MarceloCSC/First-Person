using System.Collections;
using An01malia.FirstPerson.PlayerModule;
using UnityEngine;

namespace An01malia.FirstPerson.Stealth
{
    public class PlayerFootsteps : Noise
    {
        #region Fields

        [SerializeField] private float _runningDistance = 5.0f;

        [Header("Footsteps Interval")]
        [SerializeField] private float _defaultInterval = 0.5f;
        [SerializeField] private float _runningInterval = 0.2f;

        [Space]
        [SerializeField] private float _coroutineInterval = 0.1f;

        private Coroutine _footsteps;

        private CharacterController _character;

        #endregion

        #region Unity Methods

        protected override void OnEnable()
        {
        }

        private void Start()
        {
            HearingDistance = DefaultDistance;
            StartCoroutine(HandleFootsteps());
        }

        #endregion

        #region Protected Methods

        protected override void SetReferences()
        {
            base.SetReferences();
            _character = GetComponentInParent<CharacterController>();
        }

        #endregion

        #region Private Methods

        private IEnumerator HandleFootsteps()
        {
            while (gameObject.activeSelf)
            {
                if (_character.velocity.sqrMagnitude != 0.0f && _footsteps == null)
                {
                    _footsteps = StartCoroutine(FootstepsNoise());
                }
                else if (_character.velocity.sqrMagnitude < 0.1f && _footsteps != null)
                {
                    StopCoroutine(_footsteps);
                    _footsteps = null;
                }

                yield return new WaitForSeconds(_coroutineInterval);
            }
        }

        private IEnumerator FootstepsNoise()
        {
            while (_character.velocity.sqrMagnitude != 0.0f)
            {
                yield return new WaitForSeconds(_defaultInterval);

                MakeNoise();
            }
        }

        #endregion
    }
}
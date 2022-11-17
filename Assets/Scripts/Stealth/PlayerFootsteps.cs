using System.Collections;
using An01malia.FirstPerson.Player;
using UnityEngine;

namespace An01malia.FirstPerson.Stealth
{
    public class PlayerFootsteps : Noise
    {
        [SerializeField] private float runningDistance = 5.0f;

        [Header("Footsteps Interval")]
        [SerializeField] private float _defaultInterval = 0.5f;
        [SerializeField] private float _runningInterval = 0.2f;

        [Space]
        [SerializeField] private float _coroutineInterval = 0.1f;

        private Coroutine _footsteps;

        #region Cached references

        private PlayerController _player;
        private CharacterController _character;

        #endregion Cached references

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

        protected override void SetReferences()
        {
            base.SetReferences();
            _player = GetComponentInParent<PlayerController>();
            _character = GetComponentInParent<CharacterController>();
        }
    }
}
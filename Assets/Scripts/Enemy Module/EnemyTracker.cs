using System.Collections;
using An01malia.FirstPerson.Core.References;
using UnityEngine;

namespace An01malia.FirstPerson.EnemyModule
{
    public class EnemyTracker : MonoBehaviour
    {
        #region Fields

        [SerializeField] private float _waitToStart = 0.5f;
        [SerializeField] private float _marginToReach = 1.5f;
        [SerializeField] private float _coroutineInterval = 0.1f;

        private Vector3 _playerLastPosition;
        private Vector3 _playerAssumedPosition;

        private EnemyController _enemy;
        private EnemyMovement _movement;
        private EnemyBehaviour _behaviour;

        #endregion

        #region Properties

        public Vector3 PlayerAssumedPosition
        {
            get => _playerAssumedPosition;
            set
            {
                _playerAssumedPosition = value;
                _playerLastPosition = _playerAssumedPosition;
            }
        }

        #endregion

        #region Unity Methods

        private void Awake()
        {
            SetReferences();
        }

        private void OnEnable()
        {
            _enemy.OnStateChanged += HandleState;
        }

        private void OnDisable()
        {
            _enemy.OnStateChanged -= HandleState;
        }

        #endregion

        #region Private Methods

        private IEnumerator TrackPlayer()
        {
            yield return new WaitForSeconds(_waitToStart);

            _movement.Destination = _playerLastPosition;

            while (Vector3.Distance(transform.position, _playerLastPosition) >= _marginToReach)
            {
                yield return new WaitForSeconds(_coroutineInterval);
            }

            _behaviour.HasTrackedPlayer = true;
        }

        private void HandleState(EnemyState currentState)
        {
            StopAllCoroutines();

            _behaviour.HasTrackedPlayer = false;

            if (currentState == EnemyState.Tracking)
            {
                if (_playerAssumedPosition == Vector3.zero)
                {
                    _playerLastPosition = Player.Transform.position;
                }

                StartCoroutine(TrackPlayer());
            }

            _playerAssumedPosition = Vector3.zero;
        }

        private void SetReferences()
        {
            _enemy = GetComponent<EnemyController>();
            _movement = GetComponent<EnemyMovement>();
            _behaviour = GetComponent<EnemyBehaviour>();
        }

        #endregion
    }
}
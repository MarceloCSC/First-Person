using An01malia.FirstPerson.PlayerModule;
using System.Collections;
using UnityEngine;

namespace An01malia.FirstPerson.EnemyModule
{
    public class EnemyChase : MonoBehaviour
    {
        #region Fields

        [SerializeField] private float _coroutineInterval = 0.25f;

        private EnemyController _enemy;
        private EnemyMovement _movement;

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

        private IEnumerator ChasePlayer()
        {
            while (gameObject.activeSelf)
            {
                _movement.Destination = Player.Transform.position;

                yield return new WaitForSeconds(_coroutineInterval);
            }
        }

        private void HandleState(EnemyState currentState)
        {
            if (currentState == EnemyState.Chasing)
            {
                StartCoroutine(ChasePlayer());
            }
            else
            {
                StopAllCoroutines();
            }
        }

        private void SetReferences()
        {
            _enemy = GetComponent<EnemyController>();
            _movement = GetComponent<EnemyMovement>();
        }

        #endregion
    }
}
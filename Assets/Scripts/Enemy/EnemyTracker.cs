using System.Collections;
using UnityEngine;

namespace An01malia.FirstPerson.Enemy
{

    public class EnemyTracker : MonoBehaviour
    {

        [SerializeField] float waitToStart = 0.5f;
        [SerializeField] float marginToReach = 1.5f;
        [SerializeField] float coroutineInterval = 0.1f;

        private Vector3 playerLastPosition;
        private Vector3 playerAssumedPosition;


        #region Properties
        public Vector3 PlayerAssumedPosition
        {
            get => playerAssumedPosition;
            set
            {
                playerAssumedPosition = value;
                playerLastPosition = playerAssumedPosition;
            }
        }
        #endregion


        #region Cached references
        private EnemyController enemy;
        private EnemyMovement movement;
        private EnemyBehaviour behaviour;
        private Transform player;
        #endregion


        private void Awake()
        {
            SetReferences();
        }

        private void OnEnable()
        {
            enemy.OnStateChanged += HandleState;
        }

        private IEnumerator TrackPlayer()
        {
            yield return new WaitForSeconds(waitToStart);

            movement.Destination = playerLastPosition;

            while (Vector3.Distance(transform.position, playerLastPosition) >= marginToReach)
            {
                yield return new WaitForSeconds(coroutineInterval);
            }

            behaviour.HasTrackedPlayer = true;
        }

        private void HandleState(EnemyState currentState)
        {
            StopAllCoroutines();
            behaviour.HasTrackedPlayer = false;

            if (currentState == EnemyState.Tracking)
            {
                if (playerAssumedPosition == Vector3.zero)
                {
                    playerLastPosition = player.position;
                }

                StartCoroutine(TrackPlayer());
            }

            playerAssumedPosition = Vector3.zero;
        }

        private void OnDisable()
        {
            enemy.OnStateChanged -= HandleState;
        }

        private void SetReferences()
        {
            enemy = GetComponent<EnemyController>();
            movement = GetComponent<EnemyMovement>();
            behaviour = GetComponent<EnemyBehaviour>();
            player = References.PlayerTransform;
        }

    }

}

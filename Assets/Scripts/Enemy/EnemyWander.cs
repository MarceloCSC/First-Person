using System.Collections;
using UnityEngine;

namespace An01malia.FirstPerson.Enemy
{

    public class EnemyWander : MonoBehaviour
    {

        [SerializeField] float waitAtDestination = 2.0f;
        [SerializeField] float marginToReach = 1.5f;
        [SerializeField] float coroutineInterval = 0.1f;

        [Header("Wandering Radius")]
        [SerializeField] float maxDistance = 25.0f;
        [SerializeField] float minDistance = 10.0f;


        #region Cached references
        private EnemyController enemy;
        private EnemyMovement movement;
        #endregion


        private void Awake()
        {
            SetReferences();
        }

        private void OnEnable()
        {
            enemy.OnStateChanged += HandleState;
        }

        private void GetRandomDestination()
        {
            float distance = Random.Range(minDistance, maxDistance);
            Vector3 randomDestination = transform.position.GetRandomPosition(distance);

            StartCoroutine(SetDestination(randomDestination));
        }

        private IEnumerator SetDestination(Vector3 destination)
        {
            yield return new WaitForSeconds(waitAtDestination);

            movement.Destination = destination;

            while (Vector3.Distance(transform.position, destination) >= marginToReach)
            {
                yield return new WaitForSeconds(coroutineInterval);
            }

            GetRandomDestination();
        }

        private void HandleState(EnemyState currentState)
        {
            if (currentState == EnemyState.Suspicious)
            {
                GetRandomDestination();
            }
            else
            {
                StopAllCoroutines();
            }
        }

        private void OnDisable()
        {
            enemy.OnStateChanged -= HandleState;
        }

        private void SetReferences()
        {
            enemy = GetComponent<EnemyController>();
            movement = GetComponent<EnemyMovement>();
        }

    }

}

using System.Collections;
using UnityEngine;
using An01malia.FirstPerson.PathfinderModule;

namespace An01malia.FirstPerson.EnemyModule
{

    public class EnemyPatrol : MonoBehaviour
    {

        [SerializeField] float waitAtDestination = 2.5f;
        [SerializeField] float marginToReach = 1.5f;
        [SerializeField] float coroutineInterval = 0.1f;

        private Waypoint previousWaypoint;
        private Waypoint nextWaypoint;


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

        private void GetNearestWaypoint()
        {
            float distanceToNearest = Mathf.Infinity;

            foreach (Waypoint waypoint in WaypointManager.AllWaypoints)
            {
                float currentDistance = Vector3.Distance(transform.position, waypoint.transform.position);

                if (currentDistance < distanceToNearest)
                {
                    distanceToNearest = currentDistance;
                    nextWaypoint = waypoint;
                }
            }

            StartCoroutine(SetDestination(nextWaypoint));
        }

        private IEnumerator SetDestination(Waypoint waypoint)
        {
            movement.Destination = waypoint.transform.position;

            while (Vector3.Distance(transform.position, waypoint.transform.position) >= marginToReach)
            {
                yield return new WaitForSeconds(coroutineInterval);
            }

            yield return new WaitForSeconds(waitAtDestination);

            nextWaypoint = waypoint.ChooseWaypoint(previousWaypoint);
            previousWaypoint = waypoint;

            StartCoroutine(SetDestination(nextWaypoint));
        }

        private void HandleState(EnemyState currentState)
        {
            if (currentState == EnemyState.Patroling)
            {
                GetNearestWaypoint();
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

using UnityEngine;
using UnityEngine.AI;

namespace An01malia.FirstPerson.Enemy
{

    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyMovement : MonoBehaviour
    {

        [SerializeField] float walkSpeed = 5.0f;
        [SerializeField] float runSpeed = 8.0f;

        [Space]
        [SerializeField] bool freezeEnemy = false;

        private float currentSpeed;
        private Vector3 destination;


        #region Properties
        public Vector3 Destination
        {
            get => destination;
            set
            {
                if (destination == value || freezeEnemy == true) { return; }
                destination = value;
                navMeshAgent.speed = currentSpeed;
                navMeshAgent.SetDestination(destination);
            }
        }
        #endregion


        #region Cached references
        private EnemyController enemy;
        private NavMeshAgent navMeshAgent;
        #endregion


        private void Awake()
        {
            SetReferences();
        }

        private void OnEnable()
        {
            enemy.OnMovement += HandleState;
        }

        private void OnValidate()
        {
            if (Application.isPlaying && navMeshAgent != null)
            {
                StopAgent(freezeEnemy);
            }
        }

        private void HandleState(EnemyState currentState)
        {
            if (currentState == EnemyState.Patroling || currentState == EnemyState.Suspicious)
            {
                currentSpeed = walkSpeed;
                StopAgent(false);
            }
            else if (currentState == EnemyState.Chasing)
            {
                currentSpeed = runSpeed;
                StopAgent(false);
            }
            else if (currentState == EnemyState.Attacking || currentState == EnemyState.Idle)
            {
                StopAgent(true);
            }
        }

        private void StopAgent(bool isStopped)
        {
            navMeshAgent.isStopped = isStopped;

            if (isStopped == true)
            {
                navMeshAgent.velocity = Vector3.zero;
            }
        }

        private void OnDisable()
        {
            enemy.OnMovement -= HandleState;
        }

        private void SetReferences()
        {
            enemy = GetComponent<EnemyController>();
            navMeshAgent = GetComponent<NavMeshAgent>();
        }

    }

}

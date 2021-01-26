using System.Collections;
using UnityEngine;

namespace FirstPerson.EnemyAI
{

    public class EnemyChase : MonoBehaviour
    {

        [SerializeField] float coroutineInterval = 0.25f;


        #region Cached references
        private EnemyController enemy;
        private EnemyMovement movement;
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

        private IEnumerator ChasePlayer()
        {
            while (gameObject.activeSelf)
            {
                movement.Destination = player.position;

                yield return new WaitForSeconds(coroutineInterval);
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

        private void OnDisable()
        {
            enemy.OnStateChanged -= HandleState;
        }

        private void SetReferences()
        {
            enemy = GetComponent<EnemyController>();
            movement = GetComponent<EnemyMovement>();
            player = References.PlayerTransform;
        }

    }

}

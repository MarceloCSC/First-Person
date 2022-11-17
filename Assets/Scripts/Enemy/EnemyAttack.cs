using UnityEngine;

namespace An01malia.FirstPerson.Enemy
{

    public class EnemyAttack : MonoBehaviour
    {

        #region Cached references
        private EnemyController enemy;
        #endregion


        private void Awake()
        {
            SetReferences();
        }

        private void OnEnable()
        {
            enemy.OnStateChanged += HandleState;
        }

        private void AttackPlayer()
        {
            print("Attacking player");
        }

        private void HandleState(EnemyState currentState)
        {
            if (currentState == EnemyState.Attacking)
            {
                AttackPlayer();
            }
            else
            {

            }
        }

        private void OnDisable()
        {
            enemy.OnStateChanged -= HandleState;
        }

        private void SetReferences()
        {
            enemy = GetComponent<EnemyController>();
        }

    }

}

using UnityEngine;

namespace An01malia.FirstPerson.Enemy
{

    public class EnemyBehaviour : MonoBehaviour
    {

        [SerializeField] EnemyState startingState = default;


        #region Properties
        public bool HasNotFoundPlayer { get; set; }
        public bool HasTrackedPlayer { get; set; }
        #endregion


        #region Cached references
        private EnemyController enemy;
        #endregion


        private void Awake()
        {
            SetReferences();
        }

        private void Start()
        {
            enemy.ChangeState(startingState);
        }

        public void PlayerSpotted(bool isOutOfReach)
        {
            if (isOutOfReach == true)
            {
                enemy.ChangeState(EnemyState.Chasing);
            }
            else
            {
                enemy.ChangeState(EnemyState.Attacking);
            }
        }

        public void PlayerNotSpotted()
        {
            if (enemy.CurrentState == EnemyState.Chasing)
            {
                enemy.ChangeState(EnemyState.Tracking);
            }
            else if (enemy.CurrentState == EnemyState.Tracking && HasTrackedPlayer)
            {
                enemy.ChangeState(EnemyState.Suspicious);
            }
            else if (enemy.CurrentState == EnemyState.Suspicious && HasNotFoundPlayer)
            {
                enemy.ChangeState(EnemyState.Patroling);
            }
            else if (enemy.CurrentState == EnemyState.Attacking)
            {
                enemy.ChangeState(EnemyState.Chasing);
            }
        }

        public void PlayerHeard()
        {
            if (enemy.CurrentState != EnemyState.Chasing || enemy.CurrentState != EnemyState.Attacking)
            {
                if (enemy.Alertness == AlertState.HighAlert)
                {
                    enemy.ChangeState(EnemyState.Tracking);
                }
                else if (enemy.Alertness == AlertState.Vigilant)
                {
                    enemy.ChangeState(EnemyState.Suspicious);
                }
            }
        }

        private void SetReferences()
        {
            enemy = GetComponent<EnemyController>();
        }

    }

}

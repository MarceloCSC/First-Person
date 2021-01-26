using System.Collections;
using UnityEngine;

namespace FirstPerson.EnemyAI
{

    public class EnemyAwareness : MonoBehaviour
    {

        [Header("Active Senses")]
        [SerializeField] bool enemyIsBlind = false;
        [SerializeField] bool enemyIsDeaf = false;

        [Header("Coroutine Interval")]
        [SerializeField] float defaultInterval = 0.25f;
        [SerializeField] float highAlertInterval = 0.1f;

        private float interval;


        #region Cached references
        private EnemyController enemy;
        private EnemyBehaviour behaviour;
        private EnemySight sight;
        private EnemyHearing hearing;
        #endregion


        private void Awake()
        {
            SetReferences();
        }

        private void OnEnable()
        {
            enemy.OnAlert += ChangeSettings;
        }

        private void Start()
        {
            StartCoroutine(StartAwareness());
        }

        private IEnumerator StartAwareness()
        {
            while (gameObject.activeSelf)
            {
                if (enemyIsBlind || !sight.LookForPlayer())
                {
                    behaviour.PlayerNotSpotted();
                }

                yield return new WaitForSeconds(interval);
            }
        }

        private void ChangeSettings(AlertState alertState)
        {
            if (alertState == AlertState.HighAlert)
            {
                interval = highAlertInterval;
            }
            else if (alertState == AlertState.Vigilant)
            {
                interval = defaultInterval;
            }
        }

        private void OnDisable()
        {
            enemy.OnAlert -= ChangeSettings;
        }

        private void SetReferences()
        {
            enemy = GetComponentInParent<EnemyController>();
            behaviour = GetComponentInParent<EnemyBehaviour>();
            sight = enemyIsBlind ? null : GetComponent<EnemySight>();
            hearing = enemyIsDeaf ? null : GetComponent<EnemyHearing>();
        }

    }

}

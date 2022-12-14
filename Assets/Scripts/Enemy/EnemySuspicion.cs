using System.Collections;
using UnityEngine;

namespace An01malia.FirstPerson.EnemyModule
{

    public class EnemySuspicion : MonoBehaviour
    {

        [SerializeField] float suspicionDuration = 20.0f;


        #region Cached references
        private EnemyController enemy;
        private EnemyBehaviour behaviour;
        #endregion


        private void Awake()
        {
            SetReferences();
        }

        private void OnEnable()
        {
            enemy.OnStateChanged += HandleState;
        }

        private IEnumerator SuspicionTimer()
        {
            yield return new WaitForSeconds(suspicionDuration);

            behaviour.HasNotFoundPlayer = true;
        }

        private void HandleState(EnemyState currentState)
        {
            if (currentState == EnemyState.Suspicious)
            {
                StartCoroutine(SuspicionTimer());
            }
            else
            {
                StopAllCoroutines();
                behaviour.HasNotFoundPlayer = false;
            }
        }

        private void OnDisable()
        {
            enemy.OnStateChanged -= HandleState;
        }

        private void SetReferences()
        {
            enemy = GetComponent<EnemyController>();
            behaviour = GetComponent<EnemyBehaviour>();
        }

    }

}

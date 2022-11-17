using UnityEngine;

namespace An01malia.FirstPerson.Enemy
{

    [RequireComponent(typeof(EnemyAwareness))]
    public class EnemyHearing : MonoBehaviour
    {

        [Header("Hearing Treshold")]
        [Range(-1, 1)]
        [SerializeField] float defaultTreshold = 0.0f;
        [Range(-1, 1)]
        [SerializeField] float highAlertTreshold = -1.0f;
        [Range(-1, 1)]
        [SerializeField] float distractedTreshold = 1.0f;

        private float hearingTreshold;


        #region Cached references
        private EnemyController enemy;
        private EnemyBehaviour behaviour;
        private EnemyTracker tracker;
        #endregion


        private void Awake()
        {
            SetReferences();
        }

        private void OnEnable()
        {
            enemy.OnAlert += ChangeSettings;
        }

        public void HearNoise(float noiseHeard, Vector3 origin)
        {
            if (noiseHeard > hearingTreshold)
            {
                tracker.PlayerAssumedPosition = CalculatePosition(noiseHeard, origin);
                behaviour.PlayerHeard();
            }
        }

        private Vector3 CalculatePosition(float noiseHeard, Vector3 origin)
        {
            Vector3 assumedPosition;

            if (noiseHeard < 0.0f)
            {
                float distance = (origin - transform.position).magnitude;
                float assumedDistance = distance * Mathf.Abs(noiseHeard);

                assumedPosition = (Random.insideUnitSphere * assumedDistance) + origin;
                assumedPosition.y = 0.0f;
            }
            else
            {
                assumedPosition = origin;
            }

            return assumedPosition;
        }

        private void ChangeSettings(AlertState alertState)
        {
            if (alertState == AlertState.HighAlert)
            {
                hearingTreshold = highAlertTreshold;
            }
            else if (alertState == AlertState.Vigilant)
            {
                hearingTreshold = defaultTreshold;
            }
            else
            {
                hearingTreshold = distractedTreshold;
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
            tracker = GetComponentInParent<EnemyTracker>();
        }

    }

}

using UnityEngine;
using An01malia.FirstPerson.Enemy;

namespace An01malia.FirstPerson.Stealth
{

    public class Noise : MonoBehaviour
    {

        [SerializeField] float gizmosRadius = 1.0f;
        [SerializeField] bool toggleGizmos = false;

        [Header("Hearing Distance")]
        [SerializeField] protected float defaultDistance = 10.0f;

        protected static readonly float maxNoise = 1.0f;
        protected Vector3 position;
        protected float hearingDistance;


        #region Cached references
        private LayerMask enemyLayer;
        private LayerMask obstacleLayer;
        #endregion


        private void Awake()
        {
            SetReferences();
        }

        protected virtual void OnEnable()
        {
            hearingDistance = defaultDistance;
            MakeNoise();
        }

        protected void MakeNoise()
        {
            position = transform.position;
            Collider[] enemies = Physics.OverlapSphere(position, hearingDistance, enemyLayer);

            foreach (Collider enemy in enemies)
            {
                Vector3 direction = position.GetDirectionOf(enemy.transform.position, out float enemyDistance);
                Ray ray = new Ray(position, direction);

                float noiseHeard = CalculateNoiseHeard(enemyDistance);

                if (Physics.Raycast(ray, enemyDistance, obstacleLayer))
                {
                    RaycastHit[] hits = Physics.RaycastAll(ray, enemyDistance, obstacleLayer);

                    noiseHeard -= hits.Length * maxNoise / 2;
                }

                if (enemy.GetComponentInChildren<EnemyHearing>() != null)
                {
                    enemy.GetComponentInChildren<EnemyHearing>().HearNoise(noiseHeard, position);
                }
            }
        }

        private float CalculateNoiseHeard(float enemyDistance)
        {
            float noiseHeard = maxNoise;
            float multiplier = (hearingDistance - enemyDistance) / hearingDistance;

            noiseHeard *= multiplier;

            return noiseHeard;
        }

        private void OnDrawGizmos()
        {
            if (toggleGizmos)
            {
                Gizmos.color = Color.cyan;
                Gizmos.DrawWireSphere(transform.position, gizmosRadius);

                hearingDistance = defaultDistance;

                Gizmos.color = Color.blue;
                Gizmos.DrawWireSphere(transform.position, hearingDistance);
            }
        }

        protected virtual void SetReferences()
        {
            enemyLayer = LayerMask.GetMask(Layer.Enemy);
            obstacleLayer = LayerMask.GetMask(Layer.Obstacle);
        }

    }

}

using An01malia.FirstPerson.Core;
using An01malia.FirstPerson.EnemyModule;
using UnityEngine;

namespace An01malia.FirstPerson.StealthModule
{
    public class Noise : MonoBehaviour
    {
        #region Fields

        [SerializeField] private float _gizmosRadius = 1.0f;
        [SerializeField] private bool _toggleGizmos;

        [Header("Hearing Distance")]
        [SerializeField] protected float DefaultDistance = 10.0f;

        protected static readonly float MaxNoise = 1.0f;
        protected float HearingDistance;
        protected Vector3 Position;

        private LayerMask _enemyLayer;
        private LayerMask _obstacleLayer;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            SetReferences();
        }

        protected virtual void OnEnable()
        {
            HearingDistance = DefaultDistance;
            MakeNoise();
        }

        private void OnDrawGizmos()
        {
            if (_toggleGizmos)
            {
                Gizmos.color = Color.cyan;
                Gizmos.DrawWireSphere(transform.position, _gizmosRadius);

                HearingDistance = DefaultDistance;

                Gizmos.color = Color.blue;
                Gizmos.DrawWireSphere(transform.position, HearingDistance);
            }
        }

        #endregion

        #region Protected Methods

        protected void MakeNoise()
        {
            Position = transform.position;
            Collider[] enemies = Physics.OverlapSphere(Position, HearingDistance, _enemyLayer);

            foreach (Collider enemy in enemies)
            {
                Vector3 direction = Position.GetDirectionOf(enemy.transform.position, out float enemyDistance);
                Ray ray = new Ray(Position, direction);

                float noiseHeard = CalculateNoiseHeard(enemyDistance);

                if (Physics.Raycast(ray, enemyDistance, _obstacleLayer))
                {
                    RaycastHit[] hits = Physics.RaycastAll(ray, enemyDistance, _obstacleLayer);

                    noiseHeard -= hits.Length * MaxNoise / 2;
                }

                if (enemy.GetComponentInChildren<EnemyHearing>() != null)
                {
                    enemy.GetComponentInChildren<EnemyHearing>().HearNoise(noiseHeard, Position);
                }
            }
        }

        #endregion

        #region Private Methods

        private float CalculateNoiseHeard(float enemyDistance)
        {
            float noiseHeard = MaxNoise;
            float multiplier = (HearingDistance - enemyDistance) / HearingDistance;

            noiseHeard *= multiplier;

            return noiseHeard;
        }

        protected virtual void SetReferences()
        {
            _enemyLayer = LayerMask.GetMask(Layer.Enemy);
            _obstacleLayer = LayerMask.GetMask(Layer.Obstacle);
        }

        #endregion
    }
}
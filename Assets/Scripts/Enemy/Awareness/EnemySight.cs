using An01malia.FirstPerson.Core;
using An01malia.FirstPerson.PlayerModule;
using UnityEngine;

namespace An01malia.FirstPerson.EnemyModule
{
    [RequireComponent(typeof(EnemyAwareness))]
    public class EnemySight : MonoBehaviour
    {
        #region Fields

        [Header("Awareness Radius")]
        [SerializeField] private float _defaultRadius = 20.0f;
        [SerializeField] private float _chaseRadius = 25.0f;
        [SerializeField] private float _attackRadius = 2.5f;

        [Header("FOV Cone")]
        [SerializeField] private float _defaultAngle = 60.0f;
        [SerializeField] private float _highAlertAngle = 120.0f;

        [Space]
        [SerializeField] private bool _toggleGizmos = false;

        private float _radius;
        private float _fovAngle;

        private EnemyController _enemy;
        private EnemyBehaviour _behaviour;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            SetReferences();
        }

        private void OnEnable()
        {
            _enemy.OnAlert += ChangeSettings;
        }

        private void OnDisable()
        {
            _enemy.OnAlert -= ChangeSettings;
        }

        private void OnDrawGizmos()
        {
            if (_toggleGizmos && Application.isPlaying)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawWireSphere(transform.position, _radius);

                Vector3 lineOfSightA = Quaternion.AngleAxis(_fovAngle, transform.up) * transform.forward * _radius;
                Vector3 lineOfSightB = Quaternion.AngleAxis(-_fovAngle, transform.up) * transform.forward * _radius;

                Gizmos.DrawRay(transform.position, lineOfSightA);
                Gizmos.DrawRay(transform.position, lineOfSightB);

                Vector3 direction = transform.position.GetDirectionOf(Player.Transform.position, out float playerDistance);
                float playerAngle = Vector3.Angle(direction, transform.forward);

                if (playerAngle < _fovAngle && playerDistance < _radius
                    && Physics.Raycast(transform.position, direction.normalized, out RaycastHit hit))
                {
                    if (hit.transform == Player.Transform) { Gizmos.color = Color.green; }
                    else { Gizmos.color = Color.red; }
                    Gizmos.DrawRay(transform.position, direction.normalized * _radius);
                }
            }
        }

        #endregion

        #region Public Methods

        public bool LookForPlayer()
        {
            Vector3 direction = transform.position.GetDirectionOf(Player.Transform.position, out float playerDistance);
            float playerAngle = Vector3.Angle(direction, transform.forward);

            if (playerAngle < _fovAngle && playerDistance < _radius)
            {
                DetectRaycast(direction, playerDistance);
                return true;
            }
            else { return false; }
        }

        #endregion

        #region Private Methods

        private void DetectRaycast(Vector3 direction, float playerDistance)
        {
            if (Physics.Raycast(transform.position, direction.normalized, out RaycastHit hit))
            {
                if (hit.transform == Player.Transform)
                {
                    if (playerDistance > _attackRadius)
                    {
                        _behaviour.PlayerSpotted(true);
                    }
                    else
                    {
                        _behaviour.PlayerSpotted(false);
                    }
                }
                else
                {
                    _behaviour.PlayerNotSpotted();
                }
            }
        }

        private void ChangeSettings(AlertState alertState)
        {
            if (alertState == AlertState.HighAlert)
            {
                _radius = _chaseRadius;
                _fovAngle = _highAlertAngle;
            }
            else if (alertState == AlertState.Vigilant)
            {
                _radius = _defaultRadius;
                _fovAngle = _defaultAngle;
            }
        }

        private void SetReferences()
        {
            _enemy = GetComponentInParent<EnemyController>();
            _behaviour = GetComponentInParent<EnemyBehaviour>();
        }

        #endregion
    }
}
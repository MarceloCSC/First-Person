using UnityEngine;

namespace FirstPerson.EnemyAI
{

    [RequireComponent(typeof(EnemyAwareness))]
    public class EnemySight : MonoBehaviour
    {

        [Header("Awareness Radius")]
        [SerializeField] float defaultRadius = 20.0f;
        [SerializeField] float chaseRadius = 25.0f;
        [SerializeField] float attackRadius = 2.5f;

        [Header("FOV Cone")]
        [SerializeField] float defaultAngle = 60.0f;
        [SerializeField] float highAlertAngle = 120.0f;

        [Space]
        [SerializeField] bool toggleGizmos = false;

        private float radius;
        private float fovAngle;


        #region Cached references
        private EnemyController enemy;
        private EnemyBehaviour behaviour;
        private Transform player;
        #endregion


        private void Awake()
        {
            SetReferences();
        }

        private void OnEnable()
        {
            enemy.OnAlert += ChangeSettings;
        }

        public bool LookForPlayer()
        {
            Vector3 direction = transform.position.GetDirectionOf(player.position, out float playerDistance);
            float playerAngle = Vector3.Angle(direction, transform.forward);

            if (playerAngle < fovAngle && playerDistance < radius)
            {
                DetectRaycast(direction, playerDistance);
                return true;
            }
            else { return false; }
        }

        private void DetectRaycast(Vector3 direction, float playerDistance)
        {
            if (Physics.Raycast(transform.position, direction.normalized, out RaycastHit hit))
            {
                if (hit.transform == player)
                {
                    if (playerDistance > attackRadius)
                    {
                        behaviour.PlayerSpotted(true);
                    }
                    else
                    {
                        behaviour.PlayerSpotted(false);
                    }
                }
                else
                {
                    behaviour.PlayerNotSpotted();
                }
            }
        }

        private void ChangeSettings(AlertState alertState)
        {
            if (alertState == AlertState.HighAlert)
            {
                radius = chaseRadius;
                fovAngle = highAlertAngle;
            }
            else if (alertState == AlertState.Vigilant)
            {
                radius = defaultRadius;
                fovAngle = defaultAngle;
            }
        }

        private void OnDrawGizmos()
        {
            if (toggleGizmos && Application.isPlaying)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawWireSphere(transform.position, radius);

                Vector3 lineOfSightA = Quaternion.AngleAxis(fovAngle, transform.up) * transform.forward * radius;
                Vector3 lineOfSightB = Quaternion.AngleAxis(-fovAngle, transform.up) * transform.forward * radius;

                Gizmos.DrawRay(transform.position, lineOfSightA);
                Gizmos.DrawRay(transform.position, lineOfSightB);

                Vector3 direction = transform.position.GetDirectionOf(player.position, out float playerDistance);
                float playerAngle = Vector3.Angle(direction, transform.forward);

                if (playerAngle < fovAngle && playerDistance < radius
                    && Physics.Raycast(transform.position, direction.normalized, out RaycastHit hit))
                {
                    if (hit.transform == player) { Gizmos.color = Color.green; }
                    else { Gizmos.color = Color.red; }
                    Gizmos.DrawRay(transform.position, direction.normalized * radius);
                }
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
            player = References.PlayerTransform;
        }

    }

}

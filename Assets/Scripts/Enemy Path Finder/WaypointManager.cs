using UnityEngine;

namespace FirstPerson.PathFinder
{

    public class WaypointManager : MonoBehaviour
    {

        [SerializeField] bool toggleAllGizmos = false;

        public static Waypoint[] AllWaypoints;


        private void OnValidate()
        {
            foreach (Waypoint waypoint in FindObjectsOfType<Waypoint>())
            {
                waypoint.ToggleGizmos = toggleAllGizmos;
            }
        }

        private void Awake()
        {
            AllWaypoints = FindObjectsOfType<Waypoint>();
        }

    }

}

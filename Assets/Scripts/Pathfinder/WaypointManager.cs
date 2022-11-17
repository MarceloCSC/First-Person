using UnityEngine;

namespace An01malia.FirstPerson.Pathfinder
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

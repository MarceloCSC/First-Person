using UnityEngine;

namespace An01malia.FirstPerson.Pathfinder
{
    public class WaypointManager : MonoBehaviour
    {
        #region Fields

        [SerializeField] private bool _toggleAllGizmos;

        public static Waypoint[] AllWaypoints;

        #endregion

        #region Unity Methods

        private void OnValidate()
        {
            foreach (Waypoint waypoint in FindObjectsOfType<Waypoint>())
            {
                waypoint.ToggleGizmos = _toggleAllGizmos;
            }
        }

        private void Awake()
        {
            AllWaypoints = FindObjectsOfType<Waypoint>();
        }

        #endregion
    }
}
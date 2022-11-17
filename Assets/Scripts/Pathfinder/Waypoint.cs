using System.Collections.Generic;
using UnityEngine;

namespace An01malia.FirstPerson.Pathfinder
{

    public class Waypoint : MonoBehaviour
    {

        [SerializeField] float connectionRadius = 25.0f;

        [Space]
        [SerializeField] bool toggleGizmos = false;
        [SerializeField] float gizmosRadius = 1.0f;

        private Waypoint nextWaypoint;
        private List<Waypoint> waypointNetwork;


        #region Properties
        public bool ToggleGizmos
        {
            get => toggleGizmos;
            set
            {
                toggleGizmos = value;
            }
        }
        #endregion


        private void Start()
        {
            EstablishNetwork();
        }

        private void EstablishNetwork()
        {
            waypointNetwork = new List<Waypoint>();

            for (int i = 0; i < WaypointManager.AllWaypoints.Length; i++)
            {
                Waypoint waypoint = WaypointManager.AllWaypoints[i];

                if (waypoint != null && waypoint != this)
                {
                    if (Vector3.Distance(transform.position, waypoint.transform.position) <= connectionRadius)
                    {
                        waypointNetwork.Add(waypoint);
                    }
                }
            }
        }

        public Waypoint ChooseWaypoint(Waypoint previousWaypoint)
        {
            if (waypointNetwork.Count == 0)
            {
                Debug.LogError("This waypoint is outside the range of any other waypoint.");
                return null;
            }
            else if (waypointNetwork.Count == 1 && waypointNetwork.Contains(previousWaypoint))
            {
                return previousWaypoint;
            }
            else
            {
                do
                {
                    int nextIndex = Random.Range(0, waypointNetwork.Count);
                    nextWaypoint = waypointNetwork[nextIndex];
                }
                while (nextWaypoint == previousWaypoint);

                return nextWaypoint;
            }
        }

        private void OnDrawGizmos()
        {
            if (toggleGizmos)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawWireSphere(transform.position, gizmosRadius);

                Gizmos.color = Color.white;
                Gizmos.DrawWireSphere(transform.position, connectionRadius);
            }
        }

    }

}

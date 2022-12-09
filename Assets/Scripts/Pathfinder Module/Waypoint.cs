using System.Collections.Generic;
using UnityEngine;

namespace An01malia.FirstPerson.PathfinderModule
{
    public class Waypoint : MonoBehaviour
    {
        #region Fields

        [SerializeField] private float _connectionRadius = 25.0f;

        [Space]
        [SerializeField] private bool _toggleGizmos;
        [SerializeField] private float _gizmosRadius = 1.0f;

        private Waypoint _nextWaypoint;
        private List<Waypoint> _waypointNetwork;

        #endregion

        #region Properties

        public bool ToggleGizmos
        {
            get => _toggleGizmos;
            set
            {
                _toggleGizmos = value;
            }
        }

        #endregion

        #region Unity Methods

        private void Start()
        {
            EstablishNetwork();
        }

        private void OnDrawGizmos()
        {
            if (_toggleGizmos)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawWireSphere(transform.position, _gizmosRadius);

                Gizmos.color = Color.white;
                Gizmos.DrawWireSphere(transform.position, _connectionRadius);
            }
        }

        #endregion

        #region Public Methods

        public Waypoint ChooseWaypoint(Waypoint previousWaypoint)
        {
            if (_waypointNetwork.Count == 0)
            {
                Debug.LogError("This waypoint is outside the range of any other waypoint.");
                return null;
            }
            else if (_waypointNetwork.Count == 1 && _waypointNetwork.Contains(previousWaypoint))
            {
                return previousWaypoint;
            }
            else
            {
                do
                {
                    int nextIndex = Random.Range(0, _waypointNetwork.Count);
                    _nextWaypoint = _waypointNetwork[nextIndex];
                }
                while (_nextWaypoint == previousWaypoint);

                return _nextWaypoint;
            }
        }

        #endregion

        #region Private Methods

        private void EstablishNetwork()
        {
            _waypointNetwork = new List<Waypoint>();

            for (int i = 0; i < WaypointManager.AllWaypoints.Length; i++)
            {
                Waypoint waypoint = WaypointManager.AllWaypoints[i];

                if (waypoint != null && waypoint != this)
                {
                    if (Vector3.Distance(transform.position, waypoint.transform.position) <= _connectionRadius)
                    {
                        _waypointNetwork.Add(waypoint);
                    }
                }
            }
        }

        #endregion
    }
}
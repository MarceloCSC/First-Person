using System.Collections.Generic;
using UnityEngine;

namespace An01malia.FirstPerson.Surroundings
{
    public class DetectCorners : MonoBehaviour
    {
        #region Fields

        [SerializeField] private float _rayLength = 1.0f;
        [SerializeField] private LayerMask _obstacleLayers;

        [Space]
        [SerializeField] private bool _toggleGizmos;

        private List<Transform> _rays;

        #endregion

        #region Unity Methods

        private void Start()
        {
            _rays = new List<Transform>();
        }

        private void Update()
        {
            DetectRaycast();
        }

        private void OnDrawGizmos()
        {
            if (_toggleGizmos)
            {
                Gizmos.color = Color.green;

                foreach (Transform child in transform)
                {
                    Gizmos.DrawRay(child.position, child.forward * _rayLength);
                }
            }
        }

        #endregion

        #region Private Methods

        private void DetectRaycast()
        {
            foreach (Transform child in transform)
            {
                Ray ray = new(child.position, child.forward);

                if (Physics.Raycast(ray, _rayLength, _obstacleLayers, QueryTriggerInteraction.Ignore))
                {
                    if (!_rays.Contains(child))
                    {
                        _rays.Add(child);
                        CheckForCorners();
                    }
                }
                else
                {
                    if (_rays.Contains(child))
                    {
                        _rays.Remove(child);
                        CheckForCorners();
                    }
                }
            }
        }

        private void CheckForCorners()
        {
            if (_rays.Count == 2)
            {
                //print("corner detected");

                foreach (Transform child in transform)
                {
                    if (_rays.Contains(child) && child.localPosition.x < 0)
                    {
                        //print("lean right");
                    }
                    else if (_rays.Contains(child) && child.localPosition.x > 0)
                    {
                        //print("lean left");
                    }
                }
            }
        }

        #endregion
    }
}
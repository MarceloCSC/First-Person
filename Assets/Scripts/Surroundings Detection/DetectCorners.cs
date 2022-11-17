using System.Collections.Generic;
using UnityEngine;

namespace An01malia.FirstPerson.Surroundings
{

    public class DetectCorners : MonoBehaviour
    {

        [SerializeField] float rayLength = 1.0f;
        [SerializeField] LayerMask obstacleLayers = default;

        [Space]
        [SerializeField] bool toggleGizmos = false;

        private List<Transform> rays;


        private void Start()
        {
            rays = new List<Transform>();
        }

        private void Update()
        {
            DetectRaycast();
        }

        private void DetectRaycast()
        {
            foreach (Transform child in transform)
            {
                Ray ray = new Ray(child.position, child.forward);

                if (Physics.Raycast(ray, rayLength, obstacleLayers, QueryTriggerInteraction.Ignore))
                {
                    if (!rays.Contains(child))
                    {
                        rays.Add(child);
                        CheckForCorners();
                    }
                }
                else
                {
                    if (rays.Contains(child))
                    {
                        rays.Remove(child);
                        CheckForCorners();
                    }
                }
            }
        }

        private void CheckForCorners()
        {
            if (rays.Count == 2)
            {
                print("corner detected");

                foreach (Transform child in transform)
                {
                    if (rays.Contains(child) && child.localPosition.x < 0)
                    {
                        print("lean right");
                    }
                    else if (rays.Contains(child) && child.localPosition.x > 0)
                    {
                        print("lean left");
                    }
                }
            }
        }


        private void OnDrawGizmos()
        {
            if (toggleGizmos)
            {
                Gizmos.color = Color.green;

                foreach (Transform child in transform)
                {
                    Gizmos.DrawRay(child.position, child.forward * rayLength);
                }
            }
        }

    }

}

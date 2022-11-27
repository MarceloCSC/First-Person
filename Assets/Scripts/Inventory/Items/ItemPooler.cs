using System.Collections.Generic;
using UnityEngine;

namespace An01malia.FirstPerson.InventoryModule
{

    public class ItemPooler : MonoBehaviour
    {

        [SerializeField] ItemDatabase itemDatabase = null;

        public Dictionary<string, GameObject> itemsToExamine;

        public static ItemPooler Instance { get; private set; }


        private void Awake()
        {
            Instance = this;
            CreatePool();
        }

        private void CreatePool()
        {
            itemsToExamine = new Dictionary<string, GameObject>();

            foreach (ItemObject item in itemDatabase.allItems)
            {
                GameObject prefab = Instantiate(item.ExaminePrefab);

                prefab.SetActive(false);
                prefab.transform.parent = transform;

                itemsToExamine.Add(item.ID, prefab);
            }
        }

    }

}

using UnityEngine;
using UnityEditor;

namespace An01malia.FirstPerson.InventoryModule
{

    public enum ItemType
    {
        Food,
        Valuable,
        Flammable,
        Readable,
        Unique,
        Misc
    }

    [CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/New Item")]
    public class ItemObject : ScriptableObject
    {

        [SerializeField] string itemName = null;
        [SerializeField] ItemType itemType = ItemType.Misc;
        [Space]
        [SerializeField] string uniqueID = null;
        [SerializeField] GameObject inGamePrefab = null;
        [SerializeField] GameObject itemToExamine = null;
        [SerializeField] Sprite icon = null;
        [Space]
        [TextArea(5, 30)]
        [SerializeField] string itemDescription = null;
        [Space]
        [Range(1, 999)]
        [SerializeField] int maxAmount = 1;


        #region Properties
        public string Name => itemName;
        public ItemType Type => itemType;
        public string ID => uniqueID;
        public GameObject Prefab => inGamePrefab;
        public GameObject ExaminePrefab => itemToExamine;
        public Sprite Icon => icon;
        public string Description => itemDescription;
        public int MaxAmount => maxAmount;
        #endregion


        private void OnValidate()
        {
            string path = AssetDatabase.GetAssetPath(this);
            uniqueID = AssetDatabase.AssetPathToGUID(path);
        }

    }

}

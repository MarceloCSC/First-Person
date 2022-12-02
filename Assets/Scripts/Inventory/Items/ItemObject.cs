using UnityEditor;
using UnityEngine;

namespace An01malia.FirstPerson.InventoryModule.Items
{
    [CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/New Item")]
    public class ItemObject : ScriptableObject
    {
        [SerializeField] private string itemName = null;
        [SerializeField] private ItemType itemType = ItemType.Misc;

        [Space]
        [SerializeField] private string uniqueID = null;
        [SerializeField] private GameObject inGamePrefab = null;
        [SerializeField] private GameObject itemToExamine = null;
        [SerializeField] private Sprite icon = null;

        [Space]
        [TextArea(5, 30)]
        [SerializeField] private string itemDescription = null;

        [Space]
        [Range(1, 999)]
        [SerializeField] private int maxAmount = 1;

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
using UnityEditor;
using UnityEngine;

namespace An01malia.FirstPerson.ItemModule
{
    [CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/New Item")]
    public class ItemObject : ScriptableObject
    {
        #region Fields

        [SerializeField] private string _itemName;
        [SerializeField] private ItemType _itemType;

        [Space]
        [SerializeField] private string _uniqueId;
        [SerializeField] private GameObject _inGamePrefab;
        [SerializeField] private GameObject _itemToInspect;
        [SerializeField] private Sprite _icon;

        [Space]
        [TextArea(5, 30)]
        [SerializeField] private string _itemDescription;

        [Space]
        [Range(1, 999)]
        [SerializeField] private int _maxAmount = 1;

        #endregion

        #region Properties

        public string Name => _itemName;
        public string Id => _uniqueId;
        public string Description => _itemDescription;
        public int MaxAmount => _maxAmount;
        public Sprite Icon => _icon;
        public GameObject Prefab => _inGamePrefab;
        public GameObject InspectPrefab => _itemToInspect;
        public ItemType Type => _itemType;

        #endregion

        #region Unity Methods

        private void OnValidate()
        {
            if (!string.IsNullOrEmpty(_uniqueId)) return;

            string path = AssetDatabase.GetAssetPath(this);

            _uniqueId = AssetDatabase.AssetPathToGUID(path);
        }

        #endregion
    }
}
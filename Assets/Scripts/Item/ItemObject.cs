using UnityEditor;
using UnityEngine;

namespace An01malia.FirstPerson.ItemModule
{
    [CreateAssetMenu(fileName = "NewItem", menuName = "Items/New Item")]
    public class ItemObject : ScriptableObject
    {
        #region Fields

        [SerializeField] private string _name;
        [SerializeField] private ItemType _itemType;

        [Space]
        [SerializeField] private string _uniqueId;
        [SerializeField] private GameObject _inGamePrefab;
        [SerializeField] private GameObject _itemToInspect;
        [SerializeField] private Sprite _icon;

        [Space]
        [TextArea(10, 30)]
        [SerializeField] private string _description;

        [Space]
        [Range(1, 999)]
        [SerializeField] private int _maxAmount = 1;

        #endregion

        #region Properties

        public string Name => _name;
        public string Id => _uniqueId;
        public string Description => _description;
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
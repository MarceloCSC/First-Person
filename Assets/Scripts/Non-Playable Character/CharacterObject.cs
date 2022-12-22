using UnityEditor;
using UnityEngine;

namespace An01malia.FirstPerson.NonPlayableCharacterModule
{
    [CreateAssetMenu(fileName = "NewCharacter", menuName = "NPCs/New Character")]
    public class CharacterObject : ScriptableObject
    {
        #region Fields

        [SerializeField] private string _characterName;

        [Space]
        [SerializeField] private string _uniqueID;
        [SerializeField] private Sprite _sprite;

        [Space]
        [TextArea(10, 30)]
        [SerializeField] private string _description;

        #endregion

        #region Properties

        public string Name => _characterName;
        public string ID => _uniqueID;
        public Sprite Sprite => _sprite;
        public string Description => _description;

        #endregion

        #region Unity Methods

        private void OnValidate()
        {
            string path = AssetDatabase.GetAssetPath(this);

            _uniqueID = AssetDatabase.AssetPathToGUID(path);
        }

        #endregion
    }
}
using DevourDev.Unity;
using DevourDev.Unity.ScriptableObjects;
using UnityEngine;

namespace DevourNovelEngine.Prototype.Characters
{
    public class CharacterReferenceSo : SoDatabaseElement, ICharacterReference
    {
        [SerializeField] private MetaInfo _metaInfo;
        [SerializeField] private Color _color;
        [SerializeField] private CharacterOnSceneBase _characterPrefab;


        public void Init(MetaInfo metaInfo, Color color, CharacterOnSceneBase prefab)
        {
            _metaInfo = metaInfo;
            _color = color;
            _characterPrefab = prefab;
        }


        public MetaInfo MetaInfo => _metaInfo;
        public Color Color => _color;


        public virtual ICharacter CreateCharacter()
        {
            var c = Instantiate(_characterPrefab);
            c.Init(this);
            return c;
        }
    }
}

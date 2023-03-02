using DevourDev.Unity.ScriptableObjects;
using UnityEngine;

namespace DevourDev.Unity.MultiCulture
{
    [CreateAssetMenu(menuName = "DevourDev/International/Culture Object")]
    public class CultureObject : SoDatabaseElement
    {
        [SerializeField] private MetaInfo _metaInfo;


        public MetaInfo MetaInfo => _metaInfo;

    }
}

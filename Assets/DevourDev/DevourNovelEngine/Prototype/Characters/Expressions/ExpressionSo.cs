using DevourDev.Unity;
using DevourDev.Unity.ScriptableObjects;
using DevourNovelEngine.Prototype.Commands;
using UnityEngine;

namespace DevourNovelEngine.Prototype.Characters.Expressions
{
    [CreateAssetMenu(menuName = NovelEngineConstants.MenuNameExpressions + "Expression")]
    public sealed class ExpressionSo : SoDatabaseElement, IExpression
    {
        [SerializeField] private MetaInfo _metaInfo;


        public MetaInfo MetaInfo => _metaInfo;
    }
}

using DevourDev.Unity;
using DevourDev.Unity.ScriptableObjects;
using DevourNovelEngine.Prototype.Commands;
using UnityEngine;

namespace DevourNovelEngine.Prototype.MiniGamesSystem
{
    //[CreateAssetMenu(menuName = NovelEngineConstants.MenuNameRoot + "Mini-Games/Key Object")]

    public abstract class MiniGameKeyObject<TKey, TContext, TState, TResult> : SoDatabaseElement
       where TKey : MiniGameKeyObject<TKey, TContext, TState, TResult>
        where TState : IMiniGameState<TKey, TContext, TState, TResult>
    {
        [SerializeField] private MetaInfo _metaInfo;


        public MetaInfo MetaInfo => _metaInfo;


        public void Init(MetaInfo metaInfo)
        {
            _metaInfo = metaInfo;
        }
    }
}

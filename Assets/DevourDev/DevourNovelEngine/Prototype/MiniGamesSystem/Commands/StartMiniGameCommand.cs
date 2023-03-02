using System.Collections.Generic;
using DevourNovelEngine.Prototype.Commands;
using DevourNovelEngine.Prototype.MiniGamesSystem;
using UnityEngine;

namespace DevourNovelEngine.Prototype.Core.Commands
{
    //[CreateAssetMenu(menuName = NovelEngineConstants.MenuNameCommands + "Mini-Games/Start Mini-Game")]
    public abstract class StartMiniGameCommand<TKey, TContext, TState, TResult>
        : CommandSo, IStartMiniGameCommand<TKey, TContext, TState, TResult>
        where TKey : MiniGameKeyObject<TKey, TContext, TState, TResult>
        where TState : IMiniGameState<TKey, TContext, TState, TResult>
    {
        [SerializeField] private TKey _miniGameKey;


        public TKey Key { get => _miniGameKey; internal set => _miniGameKey = value; }

        public abstract TContext Context { get; }


        public abstract IEnumerable<object> GetCommandsFromResult(TResult result);
    }
}

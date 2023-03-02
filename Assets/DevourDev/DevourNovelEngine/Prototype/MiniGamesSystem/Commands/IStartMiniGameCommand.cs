using System.Collections.Generic;
using DevourNovelEngine.Prototype.MiniGamesSystem;

namespace DevourNovelEngine.Prototype.Core.Commands
{
    public interface IStartMiniGameCommand<TKey, TContext, TState, TResult>
        where TKey : MiniGameKeyObject<TKey, TContext, TState, TResult>
        where TState : IMiniGameState<TKey, TContext, TState, TResult>
    {
        TKey Key { get; }
        TContext Context { get; }


        IEnumerable<object> GetCommandsFromResult(TResult result);
    }
}

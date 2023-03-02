using System;

namespace DevourNovelEngine.Prototype.MiniGamesSystem
{
    public interface IMiniGameState : IDisposable
    {
        object Key { get; }
        bool Paused { get; set; }
        bool Finished { get; }


        void Finish();
    }

    public interface IMiniGameState<TKey, TContext, TState, TResult> : IMiniGameState
        where TKey : MiniGameKeyObject<TKey, TContext, TState, TResult>
        where TState : IMiniGameState<TKey, TContext, TState, TResult>
    {
        TKey MiniGameKey { get; }
        
        object IMiniGameState.Key => MiniGameKey;


        event Action<TState, TResult> OnMiniGameFinished;


        bool TryGetResult(out TResult result);
    }

}

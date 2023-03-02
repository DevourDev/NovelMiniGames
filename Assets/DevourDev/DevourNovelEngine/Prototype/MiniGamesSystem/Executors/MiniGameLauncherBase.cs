using DevourDev.Unity.Utils;
using DevourNovelEngine.Prototype.Core.Commands;
using DevourNovelEngine.Prototype.Core.Executors;
using UnityEngine;

namespace DevourNovelEngine.Prototype.MiniGamesSystem
{
    public abstract class MiniGameLauncherBase<TCommand, TKey, TContext, TState, TResult>
        : ExecutorComponent<TCommand>
        where TCommand : StartMiniGameCommand<TKey, TContext, TState, TResult>
        where TKey : MiniGameKeyObject<TKey, TContext, TState, TResult>
        where TState : IMiniGameState<TKey, TContext, TState, TResult>
    {
        [SerializeField] private MiniGamesManager _miniGamesManager;

        [SerializeField] private bool _hideNovelUi = true;
        [SerializeField] private bool _blockNovelNavigation = true;


        protected override void ExecuteInherited(TCommand command)
        {
            var state = StartMiniGame(command.Key, command.Context);

            _miniGamesManager.RegisterActiveMiniGame<TCommand, TKey, TContext, TState, TResult>
                (command, state, _hideNovelUi, _blockNovelNavigation);
        }


        protected abstract TState StartMiniGame(TKey key, TContext context);
    }

}

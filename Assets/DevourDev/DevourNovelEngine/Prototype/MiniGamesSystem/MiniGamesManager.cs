using System.Collections.Generic;
using System.Runtime.Remoting.Contexts;
using DevourDev.Unity.Utils;
using DevourNovelEngine.Prototype.Core.Commands;
using DevourNovelEngine.Prototype.Ui;
using UnityEngine;

namespace DevourNovelEngine.Prototype.MiniGamesSystem
{
    public sealed class MiniGamesManager : MonoBehaviour
    {
        private readonly struct MiniGameRegistration
        {
            public readonly IMiniGameState MiniGameState;
            public readonly bool HideNovelUi;
            public readonly bool BlockNovelNavigation;


            public MiniGameRegistration(IMiniGameState state, bool hideNovelUi, bool blockNovelNavigation)
            {
                MiniGameState = state;
                HideNovelUi = hideNovelUi;
                BlockNovelNavigation = blockNovelNavigation;
            }
        }


        [SerializeField] private GameObjectsGroup _uiGroup;
        [SerializeField] private BlockingTarget _novelNavigationBlockingTarget;
        [SerializeField] private StoryLineManager _storyLineManager;

        private MiniGameRegistration _lastRegistered;


        public bool TryGetActiveMiniGameState(out IMiniGameState activeState)
        {
            activeState = _lastRegistered.MiniGameState;
            return activeState != null && !activeState.Finished;
        }

        public void RegisterActiveMiniGame<TCommand, TKey, TContext, TState, TResult>
            (TCommand startGameCmd, TState state, bool hideNovelUi, bool blockNovelNavigation)
        where TCommand : StartMiniGameCommand<TKey, TContext, TState, TResult>
        where TKey : MiniGameKeyObject<TKey, TContext, TState, TResult>
        where TState : IMiniGameState<TKey, TContext, TState, TResult>
        {
            _lastRegistered = new MiniGameRegistration(state, hideNovelUi, blockNovelNavigation);

            if (state.Finished)
                return;

            if (hideNovelUi)
                _uiGroup.SetActiveAll(false);

            if (blockNovelNavigation)
                _novelNavigationBlockingTarget.RegisterTask();

            state.OnMiniGameFinished += HandleMiniGameFinished;


            void HandleMiniGameFinished(TState state, TResult result)
            {
                state.OnMiniGameFinished -= HandleMiniGameFinished;

                if (hideNovelUi)
                    _uiGroup.SetActiveAll(true);

                if (blockNovelNavigation)
                    _novelNavigationBlockingTarget.UnregisterTask();

                var cmdsToExecute = startGameCmd.GetCommandsFromResult(result);

                foreach (var cmd in cmdsToExecute)
                {
                    _storyLineManager.ExecuteAction(cmd);
                }
            }
        }
    }
}

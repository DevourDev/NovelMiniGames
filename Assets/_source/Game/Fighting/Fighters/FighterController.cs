using System;
using System.Collections;
using DevourDev.Unity.Helpers;
using UnityEngine;

namespace Game.Fighting
{

    public class FighterController : FighterComponent
    {
        [SerializeField] private Animator _animator;

        private Coroutine _coroutine;
        private FightActionSo _fightAction;
        private TurnActor _turnActor;


        public FightActionSo SelectedAction => _fightAction;


        public event System.Action<FighterOnScene, FightActionSo> OnActionSelected;


        public void SelectAction(FightActionSo fightAction)
        {
            _fightAction = fightAction;
            OnActionSelected?.Invoke(Fighter, fightAction);
        }


        private void Awake()
        {
            var gm = CachingAccessors.Get<FightingGameManager>();
            gm.TurnsManager.OnNewTurnStage += TurnsManager_OnNewTurnStage;
        }

        private void TurnsManager_OnNewTurnStage(TurnsManager tm, TurnStage stage)
        {
            if (stage == TurnStage.Actions)
                Act();
        }


        private void Act()
        {
            if (_fightAction == null)
                return;

            _animator.SetTrigger(_fightAction.AnimParamID);
            float delay = _fightAction.PreCastTime;

            if (_coroutine != null)
            {
#if UNITY_EDITOR
                Debug.LogError($"coroutine != null: {_coroutine}");
#endif
                StopCoroutine(_coroutine);
            }

            if (delay > 0)
                RegisterWaiter();

            _coroutine = StartCoroutine(ActDelayed(delay));
        }

        private void RegisterWaiter()
        {
            if (_turnActor == null)
                _turnActor = gameObject.AddComponent<TurnActor>();

            _turnActor.Register();
        }

        private IEnumerator ActDelayed(float delay)
        {
            yield return new WaitForSeconds(delay);
            _coroutine = null;
            _fightAction.Cast(Fighter);

            if (delay > 0)
                _turnActor.Unregister();
        }

    }
}

using DevourDev.Unity.Helpers;
using UnityEngine;

namespace Game.Fighting
{
    public class FighterAi : MonoBehaviour
    {
        [SerializeField] private FightActionSo[] _availableActions;
        [SerializeField] private FighterController _controller;

        private FightActionSo _selectedAction;


        private void Awake()
        {
            var gm = CachingAccessors.Get<FightingGameManager>();
            gm.TurnsManager.OnNewTurn += HandleNewTurnStarted;
            gm.TurnsManager.OnNewTurnStage += HandleNewTurnStageStarted;
        }

        private void HandleNewTurnStarted(TurnsManager turnsManager, int newTurnNum)
        {
            SelectAction();
        }

        private void SelectAction()
        {
            _selectedAction = _availableActions[UnityEngine.Random.Range(0, _availableActions.Length)];
        }

        private void HandleNewTurnStageStarted(TurnsManager turnsManager, TurnStage newStage)
        {
            if (newStage == TurnStage.ActionsSelection)
                _controller.SelectAction(_selectedAction);
        }
    }
}

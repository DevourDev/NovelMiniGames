using System;
using TMPro;
using UnityEngine;

namespace Game.Fighting
{
    public class TurnsManagerView : MonoBehaviour
    {
        [SerializeField] private TurnsManager _tm;

        [SerializeField] private TextMeshProUGUI _turnNumText;
        [SerializeField] private TextMeshProUGUI _turnStageText;
        [SerializeField] private TextMeshProUGUI _nextStageCountDownText;

        private float _countDownCache = float.NegativeInfinity;
        private float _left;

        private void Awake()
        {
            _tm.OnNewTurn += HandleNewTurn;
            _tm.OnNewTurnStage += HandleNewTurnStage;
            Prewarm();
        }

        private void Prewarm()
        {
            HandleNewTurn(_tm, _tm.TurnNum);
            HandleNewTurnStage(_tm, _tm.TurnStage);
            HandleCountDown(_tm.StageTimeLeft);
        }


        private void Update()
        {
            if ((_left -= Time.deltaTime) > 0)
                return;

            HandleCountDown(_tm.StageTimeLeft);
        }


        private void HandleCountDown(float v)
        {
            _left = 0.1f;

            if (v == _countDownCache)
                return;

            _countDownCache = v;

            if (v > 0)
            {
                _nextStageCountDownText.text = $"До следующей стадии: {v:N1}";
            }
            else
            {
                if (_tm.TurnStage == TurnStage.WaitActionsEnd)
                    _nextStageCountDownText.text = "Ожидание завершения действий...";
                else
                    _nextStageCountDownText.text = "Ожидание следующей стадии...";
            }
        }

        private void HandleNewTurnStage(TurnsManager arg1, TurnStage arg2)
        {
            _turnStageText.text = TranslateStage(arg2);
        }

        private string TranslateStage(TurnStage stage)
        {
            return stage switch
            {
                TurnStage.Prepare => "Подготовка",
                TurnStage.ActionsSelection => "Выбор действий",
                TurnStage.Actions => "Бой",
                TurnStage.WaitActionsEnd => "Завершение боя",
                _ => throw new Exception()
            };
        }

        private void HandleNewTurn(TurnsManager arg1, int arg2)
        {
            _turnNumText.text = "ход #" + arg2.ToString();
        }
    }
}

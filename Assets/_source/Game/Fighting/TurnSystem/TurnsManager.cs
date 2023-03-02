using System;
using UnityEngine;

namespace Game.Fighting
{


    public class TurnsManager : MonoBehaviour
    {
        private float[] _stageDurations;

        private int _turnNum;
        private TurnStage _turnStage;
        private int _turnStagesCount;

        private int _activeActorsCount;
        private float _countDown;


        public int TurnNum => _turnNum;
        public TurnStage TurnStage => _turnStage;

        public bool WaitingForActors => _activeActorsCount > 0;
        public float StageTimeLeft => _countDown;

        public bool Active { get; private set; }


        public event System.Action<TurnsManager, int> OnNewTurn;
        public event System.Action<TurnsManager, TurnStage> OnNewTurnStage;


        public void SetStageDuration(TurnStage stage, float duration)
        {
            _stageDurations[(int)stage] = duration;
        }

        public void StartTurnsManager()
        {
            Active = true;
            _turnNum = 0;
            _turnStage = (TurnStage)_turnStagesCount - 1;
            NextStage();
        }

        public void RegisterAction()
        {
            ++_activeActorsCount;
        }

        public void RegisterActionEnd()
        {
            --_activeActorsCount;
        }


        private void Awake()
        {
            _turnStagesCount = System.Enum.GetValues(typeof(TurnStage)).Length;
            _stageDurations = new float[_turnStagesCount];
        }

        private void Update()
        {
            if (Active)
                NextStageCountDown();
        }

        private void NextStageCountDown()
        {
            _countDown -= Time.deltaTime;

            if (_countDown > 0)
                return;

            _countDown = -1f;

            if (_turnStage == TurnStage.WaitActionsEnd && WaitingForActors)
                return;

            NextStage();
        }

        private void NextStage()
        {
            bool newTurn = false;
            int turnStageIndex = (int)_turnStage + 1;

            if (turnStageIndex == _turnStagesCount)
            {
                newTurn = true;
                turnStageIndex = 0;
            }

            Debug.Log(turnStageIndex);
            _turnStage = (TurnStage)turnStageIndex;
            _countDown = GetStageDuration();

            if (newTurn)
            {
                ++_turnNum;
                OnNewTurn?.Invoke(this, _turnNum);
            }

            OnNewTurnStage?.Invoke(this, _turnStage);
        }

        private float GetStageDuration()
        {
            return _stageDurations[(int)_turnStage];

            throw new NullReferenceException($"unable to find duration for stage {_turnStage}");
        }
    }
}

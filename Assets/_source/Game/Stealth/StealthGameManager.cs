using System;
using DevourDev.Unity.Helpers;
using UnityEngine;

namespace Game.Stealth
{
    public class StealthGameManager : MonoBehaviour
    {
        public enum MiniGameResult
        {
            InProgress,
            Escaped,
            Caught,
        }


        [Tooltip("если значение Очков Стелса опустится ниже - вы будете обнаружены")]
        [SerializeField] private float _minStealthPoints;
        [Header("Stealth Agent")]
        [SerializeField] private SpyAgent _stealthAgent;
        [SerializeField] private float _initialStealthPoints = 10f;
        [SerializeField] private float _stealthPointsPerSecond = 0.3f;
        [SerializeField] private StealthBonusSource _stealthBonus;
        [Space]
        [SerializeField] private DisableEventSource _disableSource;


        public MiniGameResult Result { get; private set; }
        public int SpiedSecretsCount { get; private set; }


        public event System.Action<StealthGameManager> OnGameEnded;


        public void Escape()
        {
            EndGame(MiniGameResult.Escaped);
        }

        public void RegisterSpiedSecret()
        {
            ++SpiedSecretsCount;
        }


        private void Awake()
        {
            _stealthAgent.Init(_initialStealthPoints, _initialStealthPoints, _stealthPointsPerSecond);
            _stealthAgent.AddPermanentBonusSource(_stealthBonus);
            _stealthAgent.OnStealthPointsAmountChanged += HandleStealthPointsAmountChanged;
            _stealthAgent.OnSecretSpied += HandleSecretSpied;
        }

        private void HandleSecretSpied(SpyAgent arg1, StealthSecret arg2)
        {
            RegisterSpiedSecret();
        }

        private void HandleStealthPointsAmountChanged(SpyAgent agent, float pointsAmount)
        {
            AnalyzeStealthPointsAmount(pointsAmount);
        }

        private void AnalyzeStealthPointsAmount(float pointsAmount)
        {
            if (pointsAmount < _minStealthPoints)
            {
                EndGame(MiniGameResult.Caught);
            }
        }

        private void EndGame(MiniGameResult result)
        {
            if (Result != MiniGameResult.InProgress)
                return;

            _disableSource.Command(false);
            Result = result;
            OnGameEnded?.Invoke(this);
        }

        
    }
}

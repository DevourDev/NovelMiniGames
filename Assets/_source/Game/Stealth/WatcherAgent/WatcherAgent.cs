using System;
using UnityEngine;

namespace Game.Stealth
{
    public class WatcherAgent : MonoBehaviour
    {
        [SerializeField] private SpyAgentsDetectorModuleBase<SpyAgent> _detectorModule;

        [SerializeField] private float _stealthPointsPerSecondOfBeingDetected = 1f;
        [SerializeField] private float _visionDist = 5f;
        [SerializeField] private AnimationCurve _efficiencyOverDistanceCurve;

        [SerializeField] private float _minTimeToChangeWatchingState = 2f;
        [SerializeField] private float _maxTimeToChangeWatchingState = 7f;

        [SerializeField] private float _minTimeToChangeLookingDirection = 2f;
        [SerializeField] private float _maxTimeToChangeLookingDirection = 7f;

        private readonly SpyAgent[] _detectedAgentsBuffer = new SpyAgent[128];

        private bool _watching;
        private bool _lookingLeft;
        private float _timeToChangeWatchingState;
        private float _timeToChangeLookingDirection;


        public bool Watching => _watching;
        public bool LokingLeft => _lookingLeft;

        public event System.Action<WatcherAgent, bool> OnWatchingStateChanged;
        /// <summary>
        /// sender, is looking left
        /// </summary>
        public event System.Action<WatcherAgent, bool> OnLookingDirectionChanged;


        private void Update()
        {
            ProcessChangeWatchingStateCountDown();
            ProcessChangeLookingDirectionCountDown();

            Watch();
        }

        private void Watch()
        {
            if (!_watching)
                return;

            int count = _detectorModule.Detect(_detectedAgentsBuffer);

            if (count == 0)
                return;


            var spDelta = _stealthPointsPerSecondOfBeingDetected * Time.deltaTime;

            for (int i = 0; i < count; i++)
            {
                _detectedAgentsBuffer[i].ChangeStealthPoints(spDelta);
            }
        }

        private void ProcessChangeWatchingStateCountDown()
        {
            if ((_timeToChangeWatchingState -= Time.deltaTime) > 0)
                return;

            _watching = !_watching;
            ResetChangeWatchingStateCD();
            OnWatchingStateChanged?.Invoke(this, _watching);
        }

        private void ResetChangeWatchingStateCD()
        {
            _timeToChangeWatchingState = UnityEngine.Random.Range(
                _minTimeToChangeWatchingState, _maxTimeToChangeWatchingState);
        }

        private void ProcessChangeLookingDirectionCountDown()
        {
            if ((_timeToChangeLookingDirection -= Time.deltaTime) > 0)
                return;

            _lookingLeft = !_lookingLeft;
            ResetChangeLookingDirectionCD();
            OnLookingDirectionChanged?.Invoke(this, _lookingLeft);
        }

        private void ResetChangeLookingDirectionCD()
        {
            _timeToChangeLookingDirection = UnityEngine.Random.Range(
               _minTimeToChangeLookingDirection, _maxTimeToChangeLookingDirection);
        }
    }
}

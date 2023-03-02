using TMPro;
using UnityEngine;

namespace Game.Stealth
{
    public class WatcherAgentView : MonoBehaviour
    {
        //todo: пусть короче смотрит в разные стороны и в этих сторонах чекает
        //      (детектор поменять на сравнение x координат агента-шпиона и агента-наблюдателя)

        [SerializeField] private WatcherAgent _agent;
        [SerializeField] private Transform _rotatingTransform;
        [SerializeField] private bool _lookingLeftByDefault;

        [SerializeField] private TextMeshPro _watchingStateText;

        private Vector3 _lookingLeftScale;
        private Vector3 _lookingRightScale;


        private void Start()
        {
            _lookingLeftScale = _lookingRightScale = _rotatingTransform.localScale;

            _lookingLeftScale.x = -_lookingLeftScale.x;

            if (_lookingLeftByDefault)
                (_lookingLeftScale, _lookingRightScale) = (_lookingRightScale, _lookingLeftScale);


            HandleLookingDirectionChanged(_agent, _agent.LokingLeft);
            HandleWatchingStateChanged(_agent, _agent.Watching);


            _agent.OnLookingDirectionChanged += HandleLookingDirectionChanged;
            _agent.OnWatchingStateChanged += HandleWatchingStateChanged;
        }

        private void HandleLookingDirectionChanged(WatcherAgent watcher, bool lookingLeft)
        {
            _rotatingTransform.localScale = lookingLeft ? _lookingLeftScale : _lookingRightScale;
        }

        private void HandleWatchingStateChanged(WatcherAgent watcher, bool isWatching)
        {
            _watchingStateText.text = isWatching ? "насторожен" : "";
        }


    }
}

using DevourDev.Unity.Utils;
using DevourNovelEngine.Prototype.Core;
using DevourNovelEngine.Prototype.Core.Executors;
using UnityEngine;

namespace DevourNovelEngine.Prototype.Ui
{
    public class StoryLineManager : MonoBehaviour
    {
        [SerializeField] private Blocker _blocker;
        [SerializeField] private Blocker _waiter;
        [SerializeField] private StoryLineSo _storyLine;
        [SerializeField] private ExecutorsCompositeComponent _executorsComposite;

        private int _actionIndex;
        private bool _nextQueued;


        public void ExecuteAction(object command)
        {
            _executorsComposite.Execute(command);
        }

        public void NextAction()
        {
            if (_blocker.Blocked)
                return;

            if (_nextQueued)
                return;

            if (_waiter.Blocked)
            {
                QueueNext();
                return;
            }

            if (_storyLine.ActionsCount == 0)
            {
                Debug.LogError("Entered Story Line with 0 events!");
                return;
            }

            if (++_actionIndex == _storyLine.ActionsCount)
            {
                Debug.Log("StoryLine ended");
                ResetActionsIndex();
                NextAction();
                return;
            }

            var command = _storyLine[_actionIndex];
            ExecuteAction(command);
        }

        private void QueueNext()
        {
            _nextQueued = true;
            _waiter.OnBlockingStateChanged += HandleWaitingStateChanged;
        }

        private void HandleWaitingStateChanged(Blocker waiter, bool waiting)
        {
            if (waiting)
                return;

            _nextQueued = false;
            waiter.OnBlockingStateChanged -= HandleWaitingStateChanged;
            NextAction();
        }

        private void ResetActionsIndex()
        {
            _actionIndex = -1;
        }

        public void SetStoryLine(StoryLineSo storyLine)
        {
            _storyLine = storyLine;
            ResetActionsIndex();
            NextAction();
        }

    }
}

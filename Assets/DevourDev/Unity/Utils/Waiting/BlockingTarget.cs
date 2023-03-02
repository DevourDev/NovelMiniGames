using UnityEngine;

namespace DevourDev.Unity.Utils
{
    public class BlockingTarget : MonoBehaviour
    {
        [SerializeField] private Blocker _blocker;

        private int _blockingTasksCount;


        public void RegisterTask()
        {
            ++_blockingTasksCount;

            if(_blockingTasksCount == 1)
            {
                _blocker.RegisterBlockingTarget(this);
            }
        }

        public void UnregisterTask()
        {
            --_blockingTasksCount;

            if(_blockingTasksCount == 0)
            {
                _blocker.UnregisterBlockingTarget(this);
            }
        }
    }
}

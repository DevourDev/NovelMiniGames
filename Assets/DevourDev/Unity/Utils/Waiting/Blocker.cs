using System.Collections.Generic;
using UnityEngine;

namespace DevourDev.Unity.Utils
{
    public sealed class Blocker : MonoBehaviour
    {
        private List<BlockingTarget> _blockingTargets;


        public bool Blocked => _blockingTargets.Count > 0;


        public event System.Action<Blocker, bool> OnBlockingStateChanged;


        private void Awake()
        {
            _blockingTargets = new();
        }


        public void RegisterBlockingTarget(BlockingTarget blockingTarget)
        {
            _blockingTargets.Add(blockingTarget);

            if (_blockingTargets.Count == 1)
            {
                OnBlockingStateChanged?.Invoke(this, true);
            }
        }

        public void UnregisterBlockingTarget(BlockingTarget blockingTarget)
        {
            _blockingTargets.Remove(blockingTarget);

            if (_blockingTargets.Count == 0)
            {
                OnBlockingStateChanged?.Invoke(this, false);
            }
        }
    }
}

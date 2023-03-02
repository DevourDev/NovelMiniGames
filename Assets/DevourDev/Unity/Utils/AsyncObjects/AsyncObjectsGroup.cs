using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace DevourDev.Unity.Utils
{
    public sealed class AsyncObjectsGroup : MonoBehaviour
    {
        [SerializeField] private bool _chainWaiting;
        [SerializeField] private List<AsyncObjectComponent> _asyncGameObjects;

        private readonly List<Task> _waitingTasks = new();


        public async Task SetActiveAllAsync(bool active)
        {
            DeleteNuls();

            foreach (var asyncObject in _asyncGameObjects)
            {
                var waitingTask = asyncObject.SetActiveStateAsync(active);

                if (_chainWaiting)
                    await waitingTask;
                else
                    _waitingTasks.Add(waitingTask);
            }

            if (!_chainWaiting)
            {
                await Task.WhenAll(_waitingTasks);
                _waitingTasks.Clear();
            }
        }

        public async Task DestroyAllAsync()
        {
            DeleteNuls();

            foreach (var asyncObject in _asyncGameObjects)
            {
                var waitingTask = asyncObject.DestroyAsync();

                if (_chainWaiting)
                    await waitingTask;
                else
                    _waitingTasks.Add(waitingTask);
            }

            if (!_chainWaiting)
            {
                await Task.WhenAll(_waitingTasks);
                _waitingTasks.Clear();
            }
        }

        private void DeleteNuls()
        {
            for (int i = _asyncGameObjects.Count - 1; i >= 0; i--)
            {
                if (_asyncGameObjects[i] == null)
                    _asyncGameObjects.RemoveAt(i);
            }
        }
    }
}

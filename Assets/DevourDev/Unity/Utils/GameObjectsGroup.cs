using System.Collections.Generic;
using UnityEngine;

namespace DevourDev.Unity.Utils
{
    public sealed class GameObjectsGroup : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _gameObjects;


        public void SetActiveAll(bool active)
        {
            DeleteNuls();

            foreach (var go in _gameObjects)
            {
                go.SetActive(active);
            }
        }

        public void DestroyAll()
        {
            DeleteNuls();

            foreach (var go in _gameObjects)
            {
                Destroy(go);
            }
        }

        private void DeleteNuls()
        {
            for (int i = _gameObjects.Count - 1; i >= 0; i--)
            {
                if (_gameObjects[i] == null)
                    _gameObjects.RemoveAt(i);
            }
        }
    }
}

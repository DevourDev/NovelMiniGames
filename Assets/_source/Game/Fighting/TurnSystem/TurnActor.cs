using DevourDev.Unity.Helpers;
using UnityEngine;

namespace Game.Fighting
{
    public class TurnActor : MonoBehaviour
    {
        private TurnsManager _turnsManager;

        private bool _registered;


        private void Awake()
        {
            _turnsManager = CachingAccessors.Get<TurnsManager>();
        }

        private void OnDestroy()
        {
            if (_registered)
                Unregister();
        }

        public void Register()
        {
            if (_registered)
            {
                Debug.LogError("attempt to register while registered");
            }

            _registered = true;
            _turnsManager.RegisterAction();
        }

        public void Unregister()
        {
            if (!_registered)
            {
                Debug.LogError("attempt to unregister while not registered");
            }

            _registered = false;
            _turnsManager.RegisterActionEnd();
        }
    }
}

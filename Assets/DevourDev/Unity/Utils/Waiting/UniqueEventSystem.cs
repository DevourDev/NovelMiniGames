using UnityEngine;
using UnityEngine.EventSystems;

namespace DevourDev.Unity.Utils
{
    [RequireComponent(typeof(EventSystem)),
        DisallowMultipleComponent, DefaultExecutionOrder(-90000)]
    public sealed class UniqueEventSystem : MonoBehaviour
    {
        private void OnEnable()
        {
            var cur = EventSystem.current;

            if (cur == null || cur == GetComponent<EventSystem>())
                return;

            Destroy(gameObject);
        }
    }
}

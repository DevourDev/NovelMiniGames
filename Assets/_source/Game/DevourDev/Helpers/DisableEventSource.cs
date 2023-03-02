using UnityEngine;

namespace DevourDev.Unity.Helpers
{
    public class DisableEventSource : MonoBehaviour
    {
        public event System.Action<bool> OnCommand; //todo: rename


        public void Command(bool enable) => OnCommand?.Invoke(enable);
    }
}

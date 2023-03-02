using UnityEngine;

namespace DevourDev.Pools
{
    public abstract class ForcedReleasableComponent : MonoBehaviour
    {
        public abstract void Release();
    }
}

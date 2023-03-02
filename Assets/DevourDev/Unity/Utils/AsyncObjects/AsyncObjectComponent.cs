using System.Threading.Tasks;
using UnityEngine;

namespace DevourDev.Unity.Utils
{
    [DisallowMultipleComponent]
    public abstract class AsyncObjectComponent : MonoBehaviour, IAsyncObject
    {
        public abstract Task DestroyAsync();
        public abstract Task SetActiveStateAsync(bool activeState);
    }
}

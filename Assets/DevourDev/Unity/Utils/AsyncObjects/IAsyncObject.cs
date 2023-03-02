using System.Threading.Tasks;

namespace DevourDev.Unity.Utils
{
    public interface IAsyncObject
    {
        Task SetActiveStateAsync(bool activeState);

        Task DestroyAsync();
    }
}

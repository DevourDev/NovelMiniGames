using System;
using System.Threading;
using System.Threading.Tasks;

namespace DevourDev.Networking
{
    public interface IReceiver
    {
        Memory<byte> ReceiveBytes(int count);

        Task<Memory<byte>> ReceiveBytesAsync(int count);
        Task<Memory<byte>> ReceiveBytesAsync(int count, CancellationToken token);
    }
}

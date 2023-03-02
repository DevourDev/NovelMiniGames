using System;
using System.Threading;
using System.Threading.Tasks;

namespace DevourDev.Networking
{
    public interface ISender
    {
        void SendBytes(Memory<byte> bytes);
        Task SendBytesAsync(Memory<byte> bytes);
        Task SendBytesAsync(Memory<byte> bytes, CancellationToken token);
    }
}

using System;
using System.Threading;

namespace DevourDev.Networking
{
    public interface IContinuousReceiver<T> : IDisposable
    {
        event System.Action<IContinuousReceiver<T>, T> OnDataReceived;


        void StartReceiving();
        void StartReceiving(CancellationToken token);

        void StopReceiving();
    }
}

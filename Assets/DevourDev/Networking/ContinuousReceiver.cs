using System;
using System.Threading;

namespace DevourDev.Networking
{
    public abstract class ContinuousReceiver<T> : IContinuousReceiver<T>, IDisposable
    {
        private bool _softReceivingCancellationRequested;
        private bool _disposedValue;


        protected ContinuousReceiver()
        {
        }


        public event Action<IContinuousReceiver<T>, T> OnDataReceived;


        public void StartReceiving()
        {
            StartReceivingLoopInternal();
        }

        public void StartReceiving(CancellationToken token)
        {
            StartReceivingLoopInternal(token);
        }

        public void StopReceiving()
        {
            _softReceivingCancellationRequested = true;
        }


        private void StartReceivingLoopInternal(CancellationToken token = default)
        {
            while (!_softReceivingCancellationRequested)
            {
                var data = ReceiveData();
                OnDataReceived.Invoke(this, data); //no null check - event SHOULD have subscribers
            }
        }


        protected abstract T ReceiveData();

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    // TODO: освободить управляемое состояние (управляемые объекты)
                }

                // TODO: освободить неуправляемые ресурсы (неуправляемые объекты) и переопределить метод завершения
                // TODO: установить значение NULL для больших полей
                _disposedValue = true;
            }
        }

        // // TODO: переопределить метод завершения, только если "Dispose(bool disposing)" содержит код для освобождения неуправляемых ресурсов
        // ~ContinuousReceiver()
        // {
        //     // Не изменяйте этот код. Разместите код очистки в методе "Dispose(bool disposing)".
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Не изменяйте этот код. Разместите код очистки в методе "Dispose(bool disposing)".
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}

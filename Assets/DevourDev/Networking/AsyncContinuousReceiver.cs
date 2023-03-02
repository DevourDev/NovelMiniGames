using System;
using System.Threading;
using System.Threading.Tasks;

namespace DevourDev.Networking
{
    public abstract class AsyncContinuousReceiver<T> : IContinuousReceiver<T>, IDisposable
    {
        private bool _softReceivingCancellationRequested;
        private bool _disposedValue;


        protected AsyncContinuousReceiver()
        {
        }


        public event Action<IContinuousReceiver<T>, T> OnDataReceived;


        public void StartReceiving()
        {
            Task.Factory.StartNew(StartReceivingLoopInternal, default, TaskCreationOptions.LongRunning, TaskScheduler.Current);

        }

        public void StartReceiving(CancellationToken token)
        {
            Task.Factory.StartNew(action: () => StartReceivingLoopInternal(token), cancellationToken: default, creationOptions: TaskCreationOptions.LongRunning, scheduler: TaskScheduler.Current);

        }


        public void StopReceiving()
        {
            _softReceivingCancellationRequested = true;
        }


        private void StartReceivingLoopInternal()
        {
            while (!_softReceivingCancellationRequested)
            {
                var data = ReceiveData();
                OnDataReceived.Invoke(this, data); //no null check - event SHOULD have subscribers
            }
        }

        private void StartReceivingLoopInternal(CancellationToken token)
        {
            while (!_softReceivingCancellationRequested)
            {
                var data = ReceiveData(token);
                OnDataReceived.Invoke(this, data); //no null check - event SHOULD have subscribers
            }
        }


        protected abstract T ReceiveData();
        protected abstract T ReceiveData(CancellationToken token);

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
        // ~AsyncContinuousReceiver()
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

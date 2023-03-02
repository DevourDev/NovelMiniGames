using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace DevourDev.Networking
{
    public class DataReceiver : IReceiver, IDisposable
    {
        private readonly Socket _socket;

        private byte[] _buffer;
        private Memory<byte> _memBuffer;
        private bool _disposedValue;


        public DataReceiver(Socket socket, int bufferSize)
        {
#if DDEBUG
            if (bufferSize <= 0)
                throw new ArgumentException($"{nameof(bufferSize)} should be positive (attempt to set {bufferSize})");
#endif
            _socket = socket;
            _buffer = new byte[bufferSize];
            _memBuffer = new Memory<byte>(_buffer);
        }


        public bool DoNotDisposeSocket { get; set; }


        public Memory<byte> ReceiveBytes(int count)
        {
            for (int left = count; left > 0;)
            {
                left -= _socket.Receive(_memBuffer[(count - left)..count].Span);
            }

            return _memBuffer[..count];
        }

        public async Task<Memory<byte>> ReceiveBytesAsync(int count)
        {
            for (int left = count; left > 0;)
            {
                left -= await _socket.ReceiveAsync(_memBuffer[(count - left)..count], SocketFlags.None);
            }

            return _memBuffer[..count];
        }

        public async Task<Memory<byte>> ReceiveBytesAsync(int count, CancellationToken token)
        {
            for (int left = count; left > 0;)
            {
                left -= await _socket.ReceiveAsync(_memBuffer[(count - left)..count], SocketFlags.None, token);
            }

            return _memBuffer[..count];
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    // vTODO: ���������� ����������� ��������� (����������� �������)
                    if (!DoNotDisposeSocket)
                        ((IDisposable)_socket).Dispose();
                }

                // xTODO: ���������� ������������� ������� (������������� �������) � �������������� ����� ����������
                // vTODO: ���������� �������� NULL ��� ������� �����
                _memBuffer = null;
                _buffer = null;

                _disposedValue = true;
            }
        }

        // // xTODO: �������������� ����� ����������, ������ ���� "Dispose(bool disposing)" �������� ��� ��� ������������ ������������� ��������
        // ~DataReceiver()
        // {
        //     // �� ��������� ���� ���. ���������� ��� ������� � ������ "Dispose(bool disposing)".
        //     Dispose(disposing: false);
        // }

        void IDisposable.Dispose()
        {
            // �� ��������� ���� ���. ���������� ��� ������� � ������ "Dispose(bool disposing)".
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}

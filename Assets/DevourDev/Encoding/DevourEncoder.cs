using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace DevourDev.Encoding
{

    public class DevourEncoder : IEncoder, IDisposable
    {
        private byte[] _internalArray;
        private Memory<byte> _buffer;
        private int _position;
        private int _capacity;
        private bool _disposedValue;


        public DevourEncoder(int capacity)
        {
            _internalArray = new byte[capacity];
            _buffer = new Memory<byte>(_internalArray);
            _capacity = capacity;
        }


        public int Capacity => _capacity;

        public int Position
        {
            get => _position;
            set => SetPosition(value);
        }


        public ReadOnlyMemory<byte> GetData()
        {
            return _buffer[..(_position + 1)];
        }


        public void Clear()
        {
            _position = 0;
        }

        public void Close()
        {
            Dispose();
        }

        public void Resize(int newCapacity)
        {
            if (newCapacity == _capacity)
                return;

            if (newCapacity < _position)
                throw new ArgumentException($"position: {_position}, new capacity: {newCapacity}", nameof(newCapacity));

            _capacity = newCapacity;
            _buffer = null;

            var newArr = new byte[newCapacity];

            if (_position > 0)
            {
                var arr2 = _internalArray;
                Array.Copy(arr2, 0, newArr, 0, _position + 1);
            }

            _internalArray = newArr;
            _buffer = new Memory<byte>(_internalArray);
        }


        #region Write Methods
        /// <summary>
        /// если мы хотим записать КОЛЛЕКЦИЮ байтов
        /// (ключ или что-то подобное), то
        /// мы должны обращаться к <see cref="WriteStructs{TStruct}(TStruct[])"/>
        /// </summary>
        /// <param name="length"></param>
        public void WriteBytes(byte[] data)
        {
            data.AsMemory().CopyTo(SliceMem(data.Length));
        }

        /// <summary>
        /// если мы хотим записать КОЛЛЕКЦИЮ байтов
        /// (ключ или что-то подобное), то
        /// мы должны обращаться к <see cref="WriteStructs{TStruct}(TStruct[])"/>
        /// </summary>
        /// <param name="length"></param>
        public void WriteBytes(byte[] buffer, int start)
        {
            buffer.AsMemory(start).CopyTo(SliceMem(buffer.Length - start));
        }

        /// <summary>
        /// если мы хотим записать КОЛЛЕКЦИЮ байтов
        /// (ключ или что-то подобное), то
        /// мы должны обращаться к <see cref="WriteStructs{TStruct}(TStruct[])"/>
        /// </summary>
        /// <param name="length"></param>
        public void WriteBytes(byte[] buffer, int start, int length)
        {
            buffer.AsMemory(start, length).CopyTo(SliceMem(length));
        }

        /// <summary>
        /// если мы хотим записать КОЛЛЕКЦИЮ байтов
        /// (ключ или что-то подобное), то
        /// мы должны обращаться к <see cref="WriteStructs{TStruct}(TStruct[])"/>
        /// </summary>
        /// <param name="length"></param>
        public void WriteBytes(Span<byte> data)
        {
            data.CopyTo(Slice(data.Length));
        }

        /// <summary>
        /// если мы хотим записать КОЛЛЕКЦИЮ байтов
        /// (ключ или что-то подобное), то
        /// мы должны обращаться к <see cref="WriteStructs{TStruct}(TStruct[])"/>
        /// </summary>
        /// <param name="length"></param>
        public void WriteBytes(ReadOnlySpan<byte> data)
        {
            data.CopyTo(Slice(data.Length));
        }

        public void WriteStruct<TStruct>(TStruct data) where TStruct : struct
        {
            MemoryMarshal.Write(Slice(Marshal.SizeOf(data)), ref data);
        }


        public void WriteStructs<TStruct>(TStruct[] data) where TStruct : struct
        {
            int length = data.Length * Marshal.SizeOf(typeof(TStruct)); //bytes count
            WriteIntFast(length);
            MemoryMarshal.AsBytes(data.AsSpan()).CopyTo(Slice(length));
        }

        public void WriteEncodable(IEncodable encodable)
        {
            encodable.Encode(this);
        }

        public void WriteEncodables(IEncodable[] encodables)
        {
            var count = encodables.Length;
            WriteIntFast(count);

            for (int i = -1; ++i < count;)
            {
                encodables[i].Encode(this);
            }
        }

        #endregion


        protected void WriteIntFast(int value)
        {
            MemoryMarshal.Write(_buffer.Slice(_position, 4).Span, ref value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected Memory<byte> SliceMem(int length)
        {
            var slice = _buffer.Slice(_position, length);

#if DDEBUG
            Position += length;
#else
            _position += length;
#endif
            return slice;
        }

        protected Span<byte> Slice(int length)
        {
            return SliceMem(length).Span;
        }

        private void SetPosition(int value)
        {
#if DDEBUG
            if (value < 0 || value >= _capacity)
                throw new ArgumentOutOfRangeException(nameof(value));
#endif
            _position = value;
        }


        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    // xTODO: освободить управляемое состояние (управляемые объекты)
                }

                // xTODO: освободить неуправляемые ресурсы (неуправляемые объекты) и переопределить метод завершения
                // xTODO: установить значение NULL для больших полей
                _buffer = null;
                _internalArray = null;
                _disposedValue = true;
            }
        }

        // // xTODO: переопределить метод завершения, только если "Dispose(bool disposing)" содержит код для освобождения неуправляемых ресурсов
        // ~DevourEncoder()
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

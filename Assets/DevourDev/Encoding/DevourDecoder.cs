using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace DevourDev.Encoding
{
    //todo: доделать енумераторы
    //todo: протестить мемори и спаны)0))
    public class DevourDecoder : IDecoder, IDisposable
    {
        private sealed class EncodablesEnumerator<TEncodable> : IEnumerator<TEncodable> where TEncodable : IEncodable, new()
        {
            private DevourDecoder _decoder;
            private int _initialPos;
            private int _left;
            private TEncodable _current;


            public EncodablesEnumerator(DevourDecoder decoder)
            {
                _decoder = decoder;
                _initialPos = _decoder._position;
                _left = _decoder.ReadIntFast();
            }


            public TEncodable Current => _current;

            object IEnumerator.Current => _current;


            public void Dispose()
            {
                if (_left > 0)
                {
                    _decoder._position += _left * Marshal.SizeOf(typeof(TEncodable));
                }

                _current = default!;
                _decoder = null;
            }

            public bool MoveNext()
            {
                if (--_left >= 0)
                {
                    _current = _decoder.ReadEncodable<TEncodable>();
                    return true;
                }

                _current = default!;
                return false;
            }

            public void Reset()
            {
                _decoder._position = _initialPos;
                _left = _decoder.ReadIntFast();
                _current = default!;
            }
        }


        private sealed class StructsEnumerator<TStruct> : IEnumerator<TStruct> where TStruct : struct
        {
            private DevourDecoder _decoder;
            private int _initialPos;
            private int _left;
            private TStruct _current;


            public StructsEnumerator(DevourDecoder decoder)
            {
                _decoder = decoder;
                _initialPos = _decoder._position;
                _left = _decoder.ReadIntFast();
            }


            public TStruct Current => _current;

            object IEnumerator.Current => _current;


            public void Dispose()
            {
                if (_left > 0)
                {
                    _decoder._position += _left;
                }

                _current = default!;
                _decoder = null;
            }

            public bool MoveNext()
            {
                if (--_left >= 0)
                {
                    _current = _decoder.ReadStruct<TStruct>();
                    return true;
                }

                _current = default!;
                return false;
            }

            public void Reset()
            {
                _decoder._position = _initialPos;
                _left = _decoder.ReadIntFast();
                _current = default!;
            }
        }


        private ReadOnlyMemory<byte> _encodedData;
        private int _position;
        private bool _disposedValue;


        public int Position
        {
            get => _position;
            set => SetPosition(value);
        }

        private void SetPosition(int value)
        {
#if DDEBUG
            if (value < 0 || value >= _encodedData.Length)
                throw new ArgumentOutOfRangeException(nameof(value));
#endif
            _position = value;
        }

        public void LoadData(byte[] encodedData)
        {
            Clear();
            _encodedData = encodedData.AsMemory();
        }

        public void LoadData(byte[] encodedData, int start)
        {
            Clear();
            _encodedData = encodedData.AsMemory(start);
        }

        public void LoadData(byte[] encodedData, int start, int length)
        {
            Clear();
            _encodedData = encodedData.AsMemory(start, length);
        }

        public void LoadData(ReadOnlyMemory<byte> encodedData)
        {
            Clear();
            _encodedData = encodedData;
        }


        public void Clear()
        {
            _position = 0;
            _encodedData = null;
        }

        public void Close()
        {
            Dispose();
        }




        /// <summary>
        /// если мы хотим прочесть КОЛЛЕКЦИЮ байтов
        /// (ключ или что-то подобное), то
        /// мы должны обращаться к <see cref="ReadStructs{TStruct}"/>
        /// </summary>
        /// <param name="length"></param>
        public byte[] ReadBytes(int length)
        {
            var bytes = new byte[length];
            var mem = bytes.AsMemory();
            SliceMem(length).CopyTo(mem);
            return bytes;
        }

        /// <summary>
        /// если мы хотим прочесть КОЛЛЕКЦИЮ байтов
        /// (ключ или что-то подобное), то
        /// мы должны обращаться к <see cref="ReadStructs{TStruct}"/>
        /// </summary>
        /// <param name="length"></param>
        public ReadOnlyMemory<byte> ReadBytesAsMemory(int length)
        {
            return SliceMem(length);
        }


        public TStruct ReadStruct<TStruct>() where TStruct : struct
        {
            return MemoryMarshal.Read<TStruct>(Slice(Marshal.SizeOf<TStruct>()));
        }

        public TStruct[] ReadStructs<TStruct>() where TStruct : struct
        {
            int length = ReadIntFast(); // bytes count
            return MemoryMarshal.Cast<byte, TStruct>(Slice(length)).ToArray();
        }

        public int ReadStructsNonAlloc<TStruct>(TStruct[] buffer) where TStruct : struct
        {
            int length = ReadIntFast();
            var span = MemoryMarshal.Cast<byte, TStruct>(Slice(length));
            span.CopyTo(buffer.AsMemory().Span);
            return span.Length;
        }

        public int ReadStructsNonAlloc<TStruct>(TStruct[] buffer, int start) where TStruct : struct
        {
            int length = ReadIntFast();
            var span = MemoryMarshal.Cast<byte, TStruct>(Slice(length));
            span.CopyTo(buffer.AsMemory(start).Span);
            return span.Length;
        }

        public int ReadStructsNonAlloc<TStruct>(List<TStruct> buffer) where TStruct : struct
        {
            int length = ReadIntFast();
            var span = MemoryMarshal.Cast<byte, TStruct>(Slice(length));

            foreach (var item in span)
            {
                buffer.Add(item);
            }

            return span.Length;
        }


        protected int ReadIntFast()
        {
            return MemoryMarshal.Read<int>(Slice(4));
        }

        protected int PeekIntFast()
        {
            return MemoryMarshal.Read<int>(_encodedData.Slice(_position, 4).Span);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected ReadOnlyMemory<byte> SliceMem(int length)
        {
            var slice = _encodedData.Slice(_position, length);

#if DDEBUG
            Position += length;
#else
            _position += length;
#endif
            return slice;
        }

        protected ReadOnlySpan<byte> Slice(int length)
        {
            return SliceMem(length).Span;
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
                _disposedValue = true;
            }
        }

        // // xTODO: переопределить метод завершения, только если "Dispose(bool disposing)" содержит код для освобождения неуправляемых ресурсов
        // ~DevourDecoder()
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

        public IEnumerator<TStruct> GetReadingStructsEnumerator<TStruct>() where TStruct : struct
        {
            return new StructsEnumerator<TStruct>(this);
        }

        public TEncodable ReadEncodable<TEncodable>() where TEncodable : IEncodable, new()
        {
            var inst = new TEncodable();
            inst.Decode(this);
            return inst;
        }

        public TEncodable[] ReadEncodables<TEncodable>() where TEncodable : IEncodable, new()
        {
            TEncodable[] result = new TEncodable[PeekIntFast()];
            ReadEncodablesNonAlloc(result);
            return result;
        }

        public int ReadEncodablesNonAlloc<TEncodable>(TEncodable[] buffer) where TEncodable : IEncodable, new()
        {
            return ReadEncodablesNonAlloc(buffer, 0);
        }

        public int ReadEncodablesNonAlloc<TEncodable>(TEncodable[] buffer, int start) where TEncodable : IEncodable, new()
        {
            int count = ReadIntFast();

            for (int i = start - 1; ++i < count; buffer[i] = ReadEncodable<TEncodable>()) //TODO: test performance vs manual inlining
            {
            }

            return count;
        }

        public int ReadEncodablesNonAlloc<TEncodable>(List<TEncodable> buffer) where TEncodable : IEncodable, new()
        {
            int count = ReadIntFast();

            for (int i = -1; ++i < count; buffer.Add(ReadEncodable<TEncodable>())) //TODO: test performance vs manual inlining
            {
            }

            return count;
        }

        public IEnumerator<TEncodable> GetReadingEncodablesEnumerator<TEncodable>() where TEncodable : IEncodable, new()
        {
            return new EncodablesEnumerator<TEncodable>(this);
        }
    }
}

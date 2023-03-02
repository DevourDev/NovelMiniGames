using System;

namespace DevourDev.Encoding
{
    public interface IEncoder
    {
        /// <summary>
        /// buffer length
        /// </summary>
        int Capacity { get; }

        int Position { get; set; }


        ReadOnlyMemory<byte> GetData();
        void Clear();
        void Close();


        void WriteBytes(byte[] data);
        void WriteBytes(byte[] buffer, int start);
        void WriteBytes(byte[] buffer, int start, int length);
        void WriteBytes(Span<byte> data);
        void WriteBytes(ReadOnlySpan<byte> data);
        /// <typeparam name="TStruct">should not
        /// contain references</typeparam>
        /// <param name="data"></param>
        void WriteStruct<TStruct>(TStruct data) where TStruct : struct;
        void WriteStructs<TStruct>(TStruct[] data) where TStruct : struct;

        void WriteEncodable(IEncodable encodable);
        void WriteEncodables(IEncodable[] encodables);



    }
}

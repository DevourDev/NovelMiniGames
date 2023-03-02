using System;
using System.Collections.Generic;

namespace DevourDev.Encoding
{
    public interface IDecoder
    {
        int Position { get; set; }


        void LoadData(byte[] encodedData);
        void LoadData(byte[] encodedData, int start);
        void LoadData(byte[] encodedData, int start, int length);
        void LoadData(ReadOnlyMemory<byte> encodedData);

        void Clear();
        void Close();

        ReadOnlyMemory<byte> ReadBytesAsMemory(int length);
        byte[] ReadBytes(int length);

        TStruct ReadStruct<TStruct>() where TStruct : struct;
        TStruct[] ReadStructs<TStruct>() where TStruct : struct;

        /// <returns>decoded items count</returns>
        int ReadStructsNonAlloc<TStruct>(TStruct[] buffer) where TStruct : struct;
        /// <returns>decoded items count</returns>
        int ReadStructsNonAlloc<TStruct>(TStruct[] buffer, int start) where TStruct : struct;
        int ReadStructsNonAlloc<TStruct>(List<TStruct> buffer) where TStruct : struct;

        IEnumerator<TStruct> GetReadingStructsEnumerator<TStruct>() where TStruct : struct;


        TEncodable ReadEncodable<TEncodable>() where TEncodable : IEncodable, new();
        TEncodable[] ReadEncodables<TEncodable>() where TEncodable : IEncodable, new();

        /// <returns>decoded items count</returns>
        int ReadEncodablesNonAlloc<TEncodable>(TEncodable[] buffer) where TEncodable : IEncodable, new();
        /// <returns>decoded items count</returns>
        int ReadEncodablesNonAlloc<TEncodable>(TEncodable[] buffer, int start) where TEncodable : IEncodable, new();
        int ReadEncodablesNonAlloc<TEncodable>(List<TEncodable> buffer) where TEncodable : IEncodable, new();

        IEnumerator<TEncodable> GetReadingEncodablesEnumerator<TEncodable>() where TEncodable : IEncodable, new();
    }
}

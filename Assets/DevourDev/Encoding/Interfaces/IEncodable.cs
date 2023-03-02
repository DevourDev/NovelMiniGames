namespace DevourDev.Encoding
{
    public interface IEncodable
    {
        void Encode(IEncoder encoder);
        void Decode(IDecoder decoder);
    }
}

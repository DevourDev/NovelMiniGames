namespace DevourNovelEngine.Prototype.Parser.RenPy.Entities
{
    public sealed class RenShowImage : IRenCommand
    {
        private readonly RenImage _img;
        private readonly RenPosition _pos;


        public RenShowImage(RenImage image, RenPosition position)
        {
            _img = image;
            _pos = position;
        }


        public RenImage Image => _img;
        public RenPosition Position => _pos;
    }
}

namespace DevourNovelEngine.Prototype.Parser.RenPy.Entities
{
    //show image
    public class RenSetBackGround : IRenCommand
    {
        private readonly RenImage _img;


        public RenSetBackGround(RenImage img)
        {
            _img = img;
        }


        public RenImage Image => _img;
    }
}

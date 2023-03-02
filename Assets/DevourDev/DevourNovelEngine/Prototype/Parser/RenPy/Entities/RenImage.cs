namespace DevourNovelEngine.Prototype.Parser.RenPy.Entities
{
    public class RenImage
    {
        private readonly string _name;


        public RenImage(string name)
        {
            _name = name;
        }


        public string Name => _name;
    }
}

namespace DevourNovelEngine.Prototype.Parser.RenPy.Entities
{
    public class RenJump : IRenCommand
    {
        private readonly RenLabel _label;


        public RenJump(RenLabel label)
        {
            _label = label;
        }


        public RenLabel Label => _label;


        public override string ToString()
        {
            return $"Jump to {_label}";
        }
    }
}

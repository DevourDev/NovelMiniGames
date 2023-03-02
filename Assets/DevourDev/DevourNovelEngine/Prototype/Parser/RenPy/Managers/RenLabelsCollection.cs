using DevourNovelEngine.Prototype.Parser.RenPy.Entities;
using DevourNovelEngine.Prototype.Parser.Utils;

namespace DevourNovelEngine.Prototype.Parser.RenPy.Managers
{
    public sealed class RenLabelsCollection : EntitiesCollection<string, RenLabel>
    {
        private RenLabel _activeLabel;


        public void RegisterCommand(IRenCommand command)
        {
            _activeLabel.AddCommand(command);
        }

        protected override void HandleEntityRegistered(RenLabel entity)
        {
            _activeLabel = entity;
        }

        protected override RenLabel CreateBogus(string symbol)
        {
            return new RenLabel(symbol);
        }

        protected override void InitBogusWithReal(RenLabel bogus, RenLabel real)
        {
            foreach (var realCmd in real.Commands)
            {
                bogus.AddCommand(realCmd);
            }
        }
    }
}

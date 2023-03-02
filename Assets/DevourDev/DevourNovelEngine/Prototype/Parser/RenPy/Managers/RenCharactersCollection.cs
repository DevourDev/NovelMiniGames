using DevourNovelEngine.Prototype.Parser.RenPy.Entities;
using DevourNovelEngine.Prototype.Parser.Utils;
using UnityEngine;

namespace DevourNovelEngine.Prototype.Parser.RenPy.Managers
{
    public sealed class RenCharactersCollection : EntitiesCollection<string, RenCharacter>
    {
        protected override RenCharacter CreateBogus(string symbol)
        {
            return new RenCharacter(symbol, string.Empty, Color.white);
        }

        protected override void InitBogusWithReal(RenCharacter bogus, RenCharacter real)
        {
            bogus.Name = real.Name;
            bogus.Color = real.Color;
        }
    }
}

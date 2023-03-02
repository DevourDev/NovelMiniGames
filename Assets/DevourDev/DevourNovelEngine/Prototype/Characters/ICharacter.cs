using System;
using DevourNovelEngine.Prototype.Variables;

namespace DevourNovelEngine.Prototype.Characters
{
    public interface ICharacter
    {
        ICharacterReference Reference { get; }
        VariablesCollection VariablesCollection { get; }
    }
}

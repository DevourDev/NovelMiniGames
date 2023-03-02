using DevourDev.Unity;

namespace DevourNovelEngine.Prototype.Characters
{
    public interface ICharacterReference
    {
        MetaInfo MetaInfo { get; }


        ICharacter CreateCharacter();
    }
}

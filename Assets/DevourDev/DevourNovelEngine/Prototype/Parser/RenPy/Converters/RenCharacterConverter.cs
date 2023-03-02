using DevourDev.Unity;
using DevourDev.Unity.MultiCulture;
using DevourNovelEngine.Prototype.Characters;
using DevourNovelEngine.Prototype.Parser.RenPy.Entities;

namespace DevourNovelEngine.Prototype.Parser.RenPy.Converters
{

    public sealed class RenCharacterConverter : SoConverterBase<RenCharacter, CharacterReferenceSo>
    {
        protected override void InitSo(CharacterReferenceSo so, RenCharacter from)
        {
            var mcText = MultiCulturalText.Create(International.CurrentCulture, from.Name);
            var metaInfo = new MetaInfo(mcText, null, null);
            so.Init(metaInfo, from.Color, null);
        }
    }
}

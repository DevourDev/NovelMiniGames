using System.Collections.Generic;
using DevourDev.Unity.MultiCulture;
using DevourNovelEngine.Prototype.Characters;
using DevourNovelEngine.Prototype.Core.Commands;
using DevourNovelEngine.Prototype.Parser.RenPy.Entities;

namespace DevourNovelEngine.Prototype.Parser.RenPy.Converters
{
    public sealed class RenDialogSlideConverter : SoConverterBase<RenShowDialogSlide, ShowDialogSlideSo>
    {
        private readonly IReadOnlyDictionary<RenCharacter, CharacterReferenceSo> _dict;


        public RenDialogSlideConverter(IReadOnlyDictionary<RenCharacter, CharacterReferenceSo> dict)
        {
            _dict = dict;
        }


        protected override void InitSo(ShowDialogSlideSo so, RenShowDialogSlide from)
        {
            CharacterReferenceSo character = null;

            if (from.Author != null)
                character = _dict[from.Author];

            var mcText = MultiCulturalText.Create(from.Text);
            so.Init(character, mcText);
        }
    }
}

using System.Collections.Generic;
using DevourDev.Unity.MultiCulture;
using DevourNovelEngine.Prototype.Characters;
using DevourNovelEngine.Prototype.Core.Commands;
using DevourNovelEngine.Prototype.Parser.RenPy.Entities;

namespace DevourNovelEngine.Prototype.Parser.RenPy.Converters
{
    public sealed class RenSelectorConverter : SoConverterBase<RenSelector, ShowSelectorSo>
    {
        private sealed class RenSelectorTitleConverter : IConverter<RenSelector.SelectorTitle, ShowSelectorSo.SelectorTitle>
        {

            private readonly IReadOnlyDictionary<RenCharacter, CharacterReferenceSo> _dict;


            public RenSelectorTitleConverter(IReadOnlyDictionary<RenCharacter, CharacterReferenceSo> dict)
            {
                _dict = dict;
            }


            public ShowSelectorSo.SelectorTitle Convert(RenSelector.SelectorTitle from)
            {
                CharacterReferenceSo character = null;

                if (from.Character != null)
                    character = _dict[from.Character];

                var mcText = MultiCulturalText.Create(from.Text);
                return new ShowSelectorSo.SelectorTitle(character, mcText);
            }
        }


        private sealed class RenSelectorVariantConverter : IConverter<RenSelector.SelectorVariant, ShowSelectorSo.SelectorVariant>
        {
            private readonly RenJumpConverter _jumpConverter;


            public RenSelectorVariantConverter(RenJumpConverter jumpConverter)
            {
                _jumpConverter = jumpConverter;
            }


            public ShowSelectorSo.SelectorVariant Convert(RenSelector.SelectorVariant from)
            {
                var text = MultiCulturalText.Create(from.Text);
                var command = _jumpConverter.Convert((RenJump)from.Command);
                return new ShowSelectorSo.SelectorVariant(text, null, command);
            }
        }


        private readonly RenSelectorTitleConverter _titleConverter;
        private readonly RenSelectorVariantConverter _variantConverter;


        public RenSelectorConverter(IReadOnlyDictionary<RenCharacter, CharacterReferenceSo> dict,
            RenJumpConverter renJumpConverter)
        {
            _titleConverter = new(dict);
            _variantConverter = new(renJumpConverter);
        }


        protected override void InitSo(ShowSelectorSo so, RenSelector from)
        {
            ShowSelectorSo.SelectorTitle title = null;

            if (from.Title != null)
                title = _titleConverter.Convert(from.Title);

            ShowSelectorSo.SelectorVariant[] variants = new ShowSelectorSo.SelectorVariant[from.Variants.Count];

            for (int i = 0; i < variants.Length; i++)
            {
                variants[i] = _variantConverter.Convert(from.Variants[i]);
            }

            so.Init(title, variants);
        }
    }
}

using DevourNovelEngine.Prototype.Core.Commands;
using DevourNovelEngine.Prototype.Parser.RenPy.Converters;

namespace DevourNovelEngine.Prototype.Parser.RenPy.Entities
{
    public sealed class RenHideImageConverter : SoConverterBase<RenHideImage, HideSpriteSo>
    {
        private readonly RenImageConverter _imageConverter;


        public RenHideImageConverter(RenImageConverter imageConverter)
        {
            _imageConverter = imageConverter;
        }


        protected override void InitSo(HideSpriteSo so, RenHideImage from)
        {
            so.Init(_imageConverter.Convert(from.Image), RenPosToRelPos.Convert(from.Position));
        }
    }
}

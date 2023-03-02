using DevourNovelEngine.Prototype.Core.Commands;
using DevourNovelEngine.Prototype.Parser.RenPy.Converters;

namespace DevourNovelEngine.Prototype.Parser.RenPy.Entities
{
    public sealed class RenShowImageConverter : SoConverterBase<RenShowImage, ShowSpriteSo>
    {
        private readonly RenImageConverter _imageConverter;


        public RenShowImageConverter(RenImageConverter imageConverter)
        {
            _imageConverter = imageConverter;
        }


        protected override void InitSo(ShowSpriteSo so, RenShowImage from)
        {
            so.Init(_imageConverter.Convert(from.Image), RenPosToRelPos.Convert(from.Position));
        }
    }
}

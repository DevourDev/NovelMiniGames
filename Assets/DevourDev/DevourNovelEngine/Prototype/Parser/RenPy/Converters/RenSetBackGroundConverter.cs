using DevourNovelEngine.Prototype.Core.Commands;
using DevourNovelEngine.Prototype.Parser.RenPy.Converters;

namespace DevourNovelEngine.Prototype.Parser.RenPy.Entities
{
    public class RenSetBackGroundConverter : SoConverterBase<RenSetBackGround, ChangeBackGroundSo>
    {
        private readonly RenImageConverter _imageConverter;


        public RenSetBackGroundConverter(RenImageConverter imageConverter)
        {
            _imageConverter = imageConverter;
        }


        protected override void InitSo(ChangeBackGroundSo so, RenSetBackGround from)
        {
            so.Init(_imageConverter.Convert(from.Image));
        }
    }
}

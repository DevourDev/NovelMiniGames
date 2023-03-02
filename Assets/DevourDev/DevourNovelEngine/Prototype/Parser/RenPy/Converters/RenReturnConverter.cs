using DevourNovelEngine.Prototype.Core.Commands;
using DevourNovelEngine.Prototype.Parser.RenPy.Entities;

namespace DevourNovelEngine.Prototype.Parser.RenPy.Converters
{
    public sealed class RenReturnConverter : SoConverterBase<RenReturnCommand, ReturnCommandSo>
    {
        protected override void InitSo(ReturnCommandSo so, RenReturnCommand from)
        {
        }
    }
}

using DevourNovelEngine.Prototype.Parser.RenPy.Entities;
using DevourNovelEngine.Prototype.Utils;

namespace DevourNovelEngine.Prototype.Parser.RenPy.Converters
{
    public static class RenPosToRelPos
    {
        public static RelativePosition Convert(RenPosition renPosition)
        {
            return renPosition switch
            {
                RenPosition.AtLeft => RelativePosition.Left,
                RenPosition.AtRight => RelativePosition.Right,
                _ => RelativePosition.Centre,
            };
        }
    }
}

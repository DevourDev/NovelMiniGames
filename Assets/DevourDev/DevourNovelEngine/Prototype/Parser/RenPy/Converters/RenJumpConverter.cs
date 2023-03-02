using System.Collections.Generic;
using DevourNovelEngine.Prototype.Core;
using DevourNovelEngine.Prototype.Core.Commands;
using DevourNovelEngine.Prototype.Parser.RenPy.Entities;

namespace DevourNovelEngine.Prototype.Parser.RenPy.Converters
{

    public sealed class RenJumpConverter : SoConverterBase<RenJump, JumpToStoryLineSo>
    {
        private readonly IReadOnlyDictionary<RenLabel, StoryLineSo> _dict;


        public RenJumpConverter(IReadOnlyDictionary<RenLabel, StoryLineSo> dict)
        {
            _dict = dict;
        }


        protected override void InitSo(JumpToStoryLineSo so, RenJump from)
        {
            so.Init(_dict[from.Label]);
        }
    }
}

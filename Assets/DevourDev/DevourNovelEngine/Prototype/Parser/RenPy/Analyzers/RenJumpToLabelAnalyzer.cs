using System;
using DevourNovelEngine.Prototype.Parser.RenPy.Entities;
using DevourNovelEngine.Prototype.Parser.RenPy.Managers;

namespace DevourNovelEngine.Prototype.Parser.RenPy.Analyzers
{
    public sealed class RenJumpToLabelAnalyzer : IAnalyzer
    {
        public const string KeyWord = "jump";

        private readonly RenLabelsCollection _renLabels;


        public RenJumpToLabelAnalyzer(RenLabelsCollection renLabels)
        {
            _renLabels = renLabels;
        }


        public bool TryAnalyze(DocLines docLines)
        {
            var line = docLines.CurrentLine;

            if (!TryDetectJump(_renLabels, line, out var command))
                return false;

            _renLabels.RegisterCommand(command);
            return true;
        }


        public static bool TryDetectJump(RenLabelsCollection renLabels, string line, out RenJump value)
        {
            value = null;

            if (!ParsingHelpers.StartsWithSkippingSpace(line, KeyWord, out var startIndex))
                return false;
            string symbol = ParsingHelpers.TextInBounds(line, startIndex + KeyWord.Length,
                   ParsingHelpers.Space, ParsingHelpers.Colon, 1, out _, true);

            if (symbol == null)
                throw new Exception($"format error: keyword {KeyWord} detected but no leading label name.");

            symbol = symbol.TrimEnd();
            var label = renLabels.GetEntity(symbol);
            value = new RenJump(label);
            return true;
        }
    }
}

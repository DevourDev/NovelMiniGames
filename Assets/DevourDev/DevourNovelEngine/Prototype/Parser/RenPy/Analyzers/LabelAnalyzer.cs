using System;
using DevourNovelEngine.Prototype.Parser.RenPy.Managers;

namespace DevourNovelEngine.Prototype.Parser.RenPy.Analyzers
{
    public sealed class LabelAnalyzer : IAnalyzer
    {
        public const string KeyWord = "label";

        private readonly RenLabelsCollection _renLabels;


        public LabelAnalyzer(RenLabelsCollection renLabels)
        {
            _renLabels = renLabels;
        }


        public bool TryAnalyze(DocLines docLines)
        {
            var line = docLines.CurrentLine;

            if (!ParsingHelpers.StartsWithSkippingSpace(line, KeyWord, out var startIndex))
                return false;
            string symbol = ParsingHelpers.TextInBounds(line, startIndex + KeyWord.Length,
                  ParsingHelpers.Space, ParsingHelpers.Colon, 1, out _);

            if (symbol == null)
                throw new Exception($"format error: keyword {KeyWord} detected but no leading label symbol.");

            _renLabels.RegisterEntity(symbol.Trim(), new Entities.RenLabel(symbol));
            return true;
        }
    }
}

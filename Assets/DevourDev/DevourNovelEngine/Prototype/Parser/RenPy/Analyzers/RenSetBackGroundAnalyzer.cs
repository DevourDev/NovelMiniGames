using System;
using DevourNovelEngine.Prototype.Parser.RenPy.Entities;
using DevourNovelEngine.Prototype.Parser.RenPy.Managers;

namespace DevourNovelEngine.Prototype.Parser.RenPy.Analyzers
{
    public sealed class RenSetBackGroundAnalyzer : IAnalyzer
    {
        public const string KeyWord = "scene";


        private readonly RenLabelsCollection _renLabels;


        public RenSetBackGroundAnalyzer(RenLabelsCollection renLabels)
        {
            _renLabels = renLabels;
        }


        public bool TryAnalyze(DocLines lines)
        {
            var line = lines.CurrentLine;

            if (!ParsingHelpers.StartsWithSkippingSpace(line, KeyWord, out var startIndex))
                return false;

            string symbol = ParsingHelpers.TextInBounds(line, startIndex + KeyWord.Length,
                   ParsingHelpers.Space, ParsingHelpers.Space, 1, out var _, true);

            if (symbol == null)
                throw new Exception($"format error: keyword {KeyWord} detected but no leading image name.");

            var image = new RenImage(symbol);
            var command = new RenSetBackGround(image);
            _renLabels.RegisterCommand(command);
            return true;
        }
    }
}

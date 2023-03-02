using System;
using DevourNovelEngine.Prototype.Parser.RenPy.Analyzers.Helpers;
using DevourNovelEngine.Prototype.Parser.RenPy.Entities;
using DevourNovelEngine.Prototype.Parser.RenPy.Managers;

namespace DevourNovelEngine.Prototype.Parser.RenPy.Analyzers
{
    public sealed class RenShowImageAnalyzer : IAnalyzer
    {
        public const string KeyWord = "show";


        private readonly RenLabelsCollection _renLabels;


        public RenShowImageAnalyzer(RenLabelsCollection renLabels)
        {
            _renLabels = renLabels;
        }


        public bool TryAnalyze(DocLines lines)
        {
            var line = lines.CurrentLine;

            if (!ParsingHelpers.StartsWithSkippingSpace(line, KeyWord, out var startIndex))
                return false;

            RenImageParsingHelpers.Parse(line, startIndex + KeyWord.Length, out var symbol, out var position);

            if (symbol == null)
                throw new Exception($"format error: keyword {KeyWord} detected but no leading image name.");

            var image = new RenImage(symbol);
            var command = new RenShowImage(image, position);
            _renLabels.RegisterCommand(command);
            return true;
        }
    }
}

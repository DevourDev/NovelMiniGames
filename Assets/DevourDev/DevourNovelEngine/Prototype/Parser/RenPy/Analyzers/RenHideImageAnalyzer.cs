using System;
using System.Collections.Generic;
using DevourNovelEngine.Prototype.Parser.RenPy.Analyzers.Helpers;
using DevourNovelEngine.Prototype.Parser.RenPy.Converters;
using DevourNovelEngine.Prototype.Parser.RenPy.Entities;
using DevourNovelEngine.Prototype.Parser.RenPy.Managers;

namespace DevourNovelEngine.Prototype.Parser.RenPy.Analyzers
{
    public sealed class RenHideImageAnalyzer : IAnalyzer
    {
        public const string KeyWord = "hide";


        private readonly RenLabelsCollection _renLabels;


        public RenHideImageAnalyzer(RenLabelsCollection renLabels)
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
            var hideCommand = new RenHideImage(image, position);
            _renLabels.RegisterCommand(hideCommand);
            return true;
        }
    }
}

using System.Collections.Generic;
using DevourNovelEngine.Prototype.Parser.RenPy.Entities;
using DevourNovelEngine.Prototype.Parser.RenPy.Managers;

namespace DevourNovelEngine.Prototype.Parser.RenPy.Analyzers
{
    public class SelectorAnalyzer : IAnalyzer
    {
        public const string KeyWord = "menu:";

        private readonly RenLabelsCollection _renLabels;
        private readonly RenCharactersCollection _renChars;


        public SelectorAnalyzer(RenLabelsCollection renLabels, RenCharactersCollection renChars)
        {
            _renLabels = renLabels;
            _renChars = renChars;
        }


        public bool TryAnalyze(DocLines docLines)
        {
            var line = docLines.CurrentLine;

            if (!ParsingHelpers.StartsWithSkippingSpace(line, KeyWord, out var startIndex))
                return false;

            RenSelector.SelectorTitle title = null;

            docLines.Next();
            line = docLines.CurrentLine.TrimEnd();

            if (!line.EndsWith(':'))
            {
                //title
                _ = DialogSlideAnalyzer.TryDetectDialog(_renChars, line, out var character, out var text);
                title = new(character, text);
                docLines.Next(); //"variant text"
                line = docLines.CurrentLine;
            }

            List<RenSelector.SelectorVariant> variants = new();
            while (true)
            {
                string variantTitle = ParsingHelpers.TextInBounds(line, ParsingHelpers.Quotes, 0, out var endIndex);

                docLines.Next();
                line = docLines.CurrentLine;

                if (!RenJumpToLabelAnalyzer.TryDetectJump(_renLabels, line, out var jump))
                    throw new System.Exception("unable to detect jump below selector variant title: " + variantTitle);

                var variant = new RenSelector.SelectorVariant(variantTitle, jump);
                variants.Add(variant);
                docLines.Next();
                line = docLines.CurrentLine;

                if (line == null)
                    break;

                if (ParsingHelpers.TabulationsCount(line, out var _) < 2)
                {
                    docLines.Prev();
                    break;
                }
            }

            RenSelector selector = new(title, variants);

            _renLabels.RegisterCommand(selector);
            return true;
        }
    }
}

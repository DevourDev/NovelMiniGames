using DevourNovelEngine.Prototype.Parser.RenPy.Entities;
using DevourNovelEngine.Prototype.Parser.RenPy.Managers;

namespace DevourNovelEngine.Prototype.Parser.RenPy.Analyzers
{
    public sealed class DialogSlideAnalyzer : IAnalyzer
    {
        private readonly RenLabelsCollection _renLabels;
        private readonly RenCharactersCollection _renChars;


        public DialogSlideAnalyzer(RenLabelsCollection renLabels, 
            RenCharactersCollection renChars)
        {
            _renLabels = renLabels;
            _renChars = renChars;
        }


        public bool TryAnalyze(DocLines docLines)
        {
            if (!TryDetectDialog(_renChars, docLines.CurrentLine, out var character, out var text))
                return false;

            var command = new RenShowDialogSlide(character,text);
            _renLabels.RegisterCommand(command);
            return true;
        }

        public static bool TryDetectDialog(RenCharactersCollection renCharacters, string line, out RenCharacter character, out string text )
        {
            character = null;
            text = null;

            line = line.Trim();

            int firstQuotesIndex = line.IndexOf(ParsingHelpers.Quotes);

            if (firstQuotesIndex > 0)
            {
                string charSymbol = line[..(firstQuotesIndex - 1)].TrimEnd();
                character = renCharacters.GetEntity(charSymbol);
            }

            text = ParsingHelpers.TextInBounds(line, '"', 0, out _);

            if (text == null)
                return false;

            return true;
        }


    }
}

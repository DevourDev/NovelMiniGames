using DevourNovelEngine.Prototype.Parser.RenPy.Entities;
using DevourNovelEngine.Prototype.Parser.RenPy.Managers;

namespace DevourNovelEngine.Prototype.Parser.RenPy.Analyzers
{
    public class RenReturnAnalyzer : IAnalyzer
    {
        public const string KeyWord = "return";

        private readonly RenLabelsCollection _renLabels;


        public RenReturnAnalyzer(RenLabelsCollection renLabels)
        {
            _renLabels = renLabels;
        }


        public bool TryAnalyze(DocLines lines)
        {
            var line = lines.CurrentLine;

            if (!ParsingHelpers.StartsWithSkippingSpace(line, KeyWord, out var startIndex))
                return false;

            RenReturnCommand command = new();
            _renLabels.RegisterCommand(command);
            return true;
        }
    }
}

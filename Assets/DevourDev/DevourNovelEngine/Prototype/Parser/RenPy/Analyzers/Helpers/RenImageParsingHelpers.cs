using DevourNovelEngine.Prototype.Parser.RenPy.Converters;
using DevourNovelEngine.Prototype.Parser.RenPy.Entities;

namespace DevourNovelEngine.Prototype.Parser.RenPy.Analyzers.Helpers
{
    public static class RenImageParsingHelpers
    {
        public static void Parse(string text, int symbolStart, out string symbol, out RenPosition position)
        {
            int symbolEnd = text.Length;

            if (RenPositionHelpers.TryParse(text, out position, out var posFirstIndex))
                symbolEnd = posFirstIndex - 1;

            symbol = text[symbolStart..symbolEnd].Trim();
        }
    }
}

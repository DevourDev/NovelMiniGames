using System;

namespace DevourNovelEngine.Prototype.Parser.RenPy.Analyzers
{
    public sealed class SingleSymbolDetector : IAnalyzer
    {
        private readonly char _symbol;


        public SingleSymbolDetector(char symbol)
        {
            _symbol = symbol;
        }


        public bool TryAnalyze(DocLines lines)
        {
            var line = lines.CurrentLine;
            int first = line.IndexOf(_symbol);

            if (first < 0)
                return false;

            int last = line.LastIndexOf(_symbol);

            if (last == first)
                throw new Exception($"single '{_symbol}' detected!");

            return false;
        }
    }
}

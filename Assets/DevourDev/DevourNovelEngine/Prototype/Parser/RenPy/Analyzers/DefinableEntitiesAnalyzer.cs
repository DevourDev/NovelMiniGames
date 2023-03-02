using System;
using System.Collections.Generic;
using DevourNovelEngine.Prototype.Parser.RenPy.Analyzers.Definables;

namespace DevourNovelEngine.Prototype.Parser.RenPy.Analyzers
{
    public class DefinableEntitiesAnalyzer : IAnalyzer
    {
        public const string KeyWord = "define";

        private readonly Dictionary<string, IDefinableEntityAnalyzer> _defineAnalyzers = new();


        public void AddDefinableAnalyzer(IDefinableEntityAnalyzer defineAnalyzer)
        {
            _defineAnalyzers.Add(defineAnalyzer.ClassName, defineAnalyzer);
        }


        public bool TryAnalyze(DocLines lines)
        {
            var line = lines.CurrentLine;

            if (!ParsingHelpers.StartsWithSkippingSpace(line, KeyWord, out var startIndex))
                return false;

            string symbol = ParsingHelpers.TextInBounds(line, startIndex + KeyWord.Length,
                ParsingHelpers.Space, ParsingHelpers.Space, 1, out var endIndex);

            if (symbol == null)
                throw new Exception($"format error: keyword {KeyWord} detected but no leading character symbol.");

            string className = ParsingHelpers.FromFirstToFirst(line, endIndex + 1, '=', '(', out endIndex);

            if (className == null)
                throw new Exception($"format error: keyword {KeyWord} and symbol {symbol} detected" +
                    $" but no leading class name.");

            className = className.Trim();
            string parameters = ParsingHelpers.FromFirstToLast(line, endIndex + 1, '(', ')', out _);

            if (!_defineAnalyzers.TryGetValue(className, out var defineAnalyzer))
            {
                UnityEngine.Debug.Log("unable to find define analyzer for class " + className);
                return false;
            }

            return defineAnalyzer.TryAnalyze(symbol, parameters);
        }
    }
}

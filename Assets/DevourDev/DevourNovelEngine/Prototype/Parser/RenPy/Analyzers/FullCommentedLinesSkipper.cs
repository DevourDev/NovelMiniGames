namespace DevourNovelEngine.Prototype.Parser.RenPy.Analyzers
{

    public class FullCommentedLinesSkipper : IAnalyzer
    {
        public const char PythonCommendSymbol = '#';


        public bool TryAnalyze(DocLines lines)
        {
            var line = lines.CurrentLine;

            foreach (char c in line)
            {
                switch (c)
                {
                    case ' ':
                        continue;
                    case PythonCommendSymbol:
                        return true;
                    default:
                        return false;
                }
            }

            return true; // empty line
        }
    }
}

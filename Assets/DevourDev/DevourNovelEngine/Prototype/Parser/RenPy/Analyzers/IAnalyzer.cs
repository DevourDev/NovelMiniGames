namespace DevourNovelEngine.Prototype.Parser.RenPy.Analyzers
{
    public interface IAnalyzer
    {
        bool TryAnalyze(DocLines lines);
    }
}

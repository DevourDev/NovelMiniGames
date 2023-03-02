namespace DevourNovelEngine.Prototype.Parser.RenPy.Analyzers.Definables
{
    public interface IDefinableEntityAnalyzer
    {
        string ClassName { get; }


        bool TryAnalyze(string symbol, string parameters);
    }
}

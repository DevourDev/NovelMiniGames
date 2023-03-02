using DevourNovelEngine.Prototype.Parser.RenPy.Entities;
using DevourNovelEngine.Prototype.Parser.RenPy.Managers;

namespace DevourNovelEngine.Prototype.Parser.RenPy.Analyzers.Definables
{
    public class DefinableCharacterAnalyzer : IDefinableEntityAnalyzer
    {
        private readonly RenCharactersCollection _renChars;


        public DefinableCharacterAnalyzer(RenCharactersCollection renChars)
        {
            _renChars = renChars;
        }


        public string ClassName => "Character";


        public bool TryAnalyze(string symbol, string parameters)
        {
            int start = parameters.IndexOf('"') + 1;
            int end = parameters.IndexOf('"', start);

            string charName = parameters[start..end];

            start = parameters.IndexOf('"', end + 1) + 1;
            end = parameters.IndexOf('"', start);

            string charColor = parameters[start..end];

            var character = new RenCharacter(symbol, charName, UnityColorUtils.FromHex255Rgb(charColor));
            _renChars.RegisterEntity(symbol, character);
            return true;
        }
    }
}

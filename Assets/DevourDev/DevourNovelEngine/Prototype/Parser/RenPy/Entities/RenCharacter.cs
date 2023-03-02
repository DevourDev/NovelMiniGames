using UnityEngine;

namespace DevourNovelEngine.Prototype.Parser.RenPy.Entities
{
    public class RenCharacter
    {
        private string _symbol;
        private string _name;
        private Color _color;


        public RenCharacter(string symbol, string name, Color color)
        {
            _symbol = symbol;
            _name = name;
            _color = color;
        }


        
        public string Symbol { get => _symbol; internal set => _symbol = value; }
        public string Name { get => _name; internal set => _name = value; }
        public Color Color { get => _color; internal set => _color = value; }

        public override string ToString()
        {
            return $"Character {_name} ({_symbol})";
        }
    }
}

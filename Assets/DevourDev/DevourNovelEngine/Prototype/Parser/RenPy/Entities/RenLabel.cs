using System.Collections.Generic;

namespace DevourNovelEngine.Prototype.Parser.RenPy.Entities
{
    public class RenLabel
    {
        private string _symbol;
        private readonly List<IRenCommand> _commands;


        public RenLabel(string symbol)
        {
            _symbol = symbol;
            _commands = new();
        }



        public string Symbol { get => _symbol; internal set => _symbol = value; }
        public IReadOnlyList<IRenCommand> Commands => _commands;


        public void AddCommand(IRenCommand command)
        {
            if (command == null)
            {
                UnityEngine.Debug.LogError("null command");
            }
            _commands.Add(command);
        }


        public override string ToString()
        {
            return $"Label {_symbol}: {_commands.Count} commands";
        }
    }
}

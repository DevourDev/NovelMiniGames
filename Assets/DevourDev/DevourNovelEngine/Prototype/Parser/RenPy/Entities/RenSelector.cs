using System.Collections.Generic;
using System.Text;

namespace DevourNovelEngine.Prototype.Parser.RenPy.Entities
{
    public sealed class RenSelector : IRenCommand
    {
        public sealed class SelectorTitle
        {
            private readonly RenCharacter _character;
            private readonly string _text;


            public SelectorTitle(RenCharacter character, string text)
            {
                _character = character;
                _text = text;
            }


            public RenCharacter Character => _character;
            public string Text => _text;


            public override string ToString()
            {
                if (_character == null)
                    return $"\"{_text}\"";

                return $"{_character}: \"{_text}\"";
            }
        }


        public sealed class SelectorVariant
        {
            private readonly string _text;
            private readonly IRenCommand _command;

            public SelectorVariant(string text, IRenCommand command)
            {
                _text = text;
                _command = command;
            }


            public string Text => _text;
            public IRenCommand Command => _command;
        }


        private readonly SelectorTitle _title;
        private readonly List<SelectorVariant> _variants;


        public RenSelector(SelectorTitle title, List<SelectorVariant> variants)
        {
            _title = title;
            _variants = variants;
        }


        public SelectorTitle Title => _title;
        public List<SelectorVariant> Variants => _variants;


        public override string ToString()
        {
            StringBuilder sb = SharedStringBuilder.StringBuilder;

            sb.Append($"Selector:");

            if (_title != null)
                sb.Append(_title.ToString());

            sb.Append("\nVariants:");

            foreach (var variant in _variants)
            {
                sb.Append($"\n{variant.Text}: {variant.Command}");
            }

            var v = sb.ToString();
            sb.Clear();
            return v;
        }
    }
}

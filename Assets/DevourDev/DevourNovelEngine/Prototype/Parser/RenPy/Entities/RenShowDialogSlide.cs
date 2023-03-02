using DevourDev.Collections.Generic;

namespace DevourNovelEngine.Prototype.Parser.RenPy.Entities
{
    public class RenShowDialogSlide : IRenCommand
    {
        private readonly RenCharacter  _author;
        private readonly string _text;


        public RenShowDialogSlide(RenCharacter author, string text)
        {
            _author = author;
            _text = text;
        }


        public RenCharacter Author => _author;
        public string Text => _text;


        public override string ToString()
        {
            if (_author == null)
                return $"\"{_text}\"";

            return $"{_author}: \"{_text}\"";
        }
    }
}

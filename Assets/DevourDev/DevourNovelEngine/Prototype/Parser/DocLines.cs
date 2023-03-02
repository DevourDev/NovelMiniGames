using System.IO;
using System.Threading.Tasks;

namespace DevourNovelEngine.Prototype.Parser
{
    public sealed class DocLines
    {
        private readonly string[] _lines;
        private int _lineIndex;


        public DocLines(string[] lines)
        {
            _lines = lines;
        }


        public int Index => _lineIndex;

        public bool Ended => _lineIndex < 0 || _lineIndex >= _lines.Length;

        public string CurrentLine
        {
            get
            {
                if (Ended)
                    return null;

                return _lines[_lineIndex];
            }
        }

        public float Progress
        {
            get
            {
                return (float)_lineIndex / _lines.Length;
            }
        }


        public void Next()
        {
            do
            {
                ++_lineIndex;
            } while (CurrentLine != null && string.IsNullOrWhiteSpace(CurrentLine));
        }

        public void Prev()
        {
            do
            {
                --_lineIndex;
            } while (CurrentLine != null && string.IsNullOrWhiteSpace(CurrentLine));
        }

        public void Reset()
        {
            _lineIndex = 0;
        }

        public void Trim()
        {
            if (Ended)
                return;

            _lines[_lineIndex] = _lines[_lineIndex].Trim();
        }

        public void TrimStart()
        {
            if (Ended)
                return;

            _lines[_lineIndex] = _lines[_lineIndex].TrimStart();
        }

        public void TrimEnd()
        {
            if (Ended)
                return;

            _lines[_lineIndex] = _lines[_lineIndex].TrimEnd();
        }

        public static async Task<DocLines> FromFileAsync(string path)
        {
            var lines = await File.ReadAllLinesAsync(path);
            return new DocLines(lines);
        }

        public static DocLines FromFile(string path)
        {
            var lines = File.ReadAllLines(path);
            return new DocLines(lines);
        }
    }
}

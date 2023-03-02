using DevourNovelEngine.Prototype.Parser.RenPy.Entities;

namespace DevourNovelEngine.Prototype.Parser.RenPy.Analyzers.Helpers
{
    public static class RenPositionHelpers
    {
        public const string AtLeft = "at left";
        public const string AtRight = "at right";


        public static bool TryParse(string v, out RenPosition position, out int firstIndex)
        {
            v = v.Trim();

            firstIndex = v.LastIndexOf("at");

            if (firstIndex < 0)
            {
                position = default;
                return false;
            }

            for (int i = firstIndex + 3; i < v.Length; i++)
            {
                var c = v[i];

                switch (c)
                {
                    case ' ':
                        continue;
                    case 'l':
                        position = RenPosition.AtLeft;
                        return true;
                    case 'r':
                        position = RenPosition.AtRight;
                        return true;
                    default:
                        position = default;
                        return false;
                }
            }

            position = default;
            return false;
        }
    }
}

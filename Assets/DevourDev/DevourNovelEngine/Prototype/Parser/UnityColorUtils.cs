using UnityEngine;

namespace DevourNovelEngine.Prototype.Parser
{
    public static class UnityColorUtils
    {
        public static Color FromHex255Rgb(string text)
        {
            int start = 0;

            for (; start < text.Length; start++)
            {
                if (char.IsLetterOrDigit(text[start]))
                    break;
            }

            if (start >= text.Length)
                return Color.white;

            float r, g, b, a;

            r = Hex255IntToNormalizedFloat(text[start..(start += 2)]);
            g = Hex255IntToNormalizedFloat(text[start..(start += 2)]);
            b = Hex255IntToNormalizedFloat(text[start..(start += 2)]);
            a = 1f;

            return new Color(r, g, b, a);
        }

        public static float Hex255IntToNormalizedFloat(string v)
        {
            int integer = int.Parse(v, System.Globalization.NumberStyles.HexNumber);
            return integer / 255f;
        }
    }
}

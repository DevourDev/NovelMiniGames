using System.Text;

namespace DevourNovelEngine.Prototype.Parser
{
    public static class SharedStringBuilder
    {
        public static StringBuilder StringBuilder { get; } = new();
    }
}

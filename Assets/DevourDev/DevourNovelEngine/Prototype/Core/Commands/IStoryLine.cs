using DevourNovelEngine.Prototype.Core.Commands;

namespace DevourNovelEngine.Prototype.Core
{
    public interface IStoryLine
    {
        CommandSo this[int index] { get; }

        int ActionsCount { get; }

        CommandSo GetAction(int index);
    }
}
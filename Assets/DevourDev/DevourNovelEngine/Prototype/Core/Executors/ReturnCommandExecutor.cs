using DevourNovelEngine.Prototype.Core.Commands;
using DevourNovelEngine.Prototype.Ui;
using UnityEngine;

namespace DevourNovelEngine.Prototype.Core.Executors
{
    public sealed class ReturnCommandExecutor : ExecutorComponent<ReturnCommandSo>
    {
        [SerializeField] private StoryLineManager _storyLineManager;
        [SerializeField] private StoryLineSo _returnTo;



        protected override void ExecuteInherited(ReturnCommandSo command)
        {
            _storyLineManager.SetStoryLine(_returnTo);
        }
    }
}

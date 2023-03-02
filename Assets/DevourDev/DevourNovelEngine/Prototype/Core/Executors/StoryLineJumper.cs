using DevourNovelEngine.Prototype.Core.Commands;
using DevourNovelEngine.Prototype.Ui;
using UnityEngine;

namespace DevourNovelEngine.Prototype.Core.Executors
{

    public sealed class StoryLineJumper : ExecutorComponent<JumpToStoryLineSo>
    {
        [SerializeField] private StoryLineManager _storyLineManager;


        protected override void ExecuteInherited(JumpToStoryLineSo command)
        {
            _storyLineManager.SetStoryLine(command.Destination);
        }
    }
}

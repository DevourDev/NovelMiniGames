using DevourNovelEngine.Prototype.Core.Commands;
using DevourNovelEngine.Prototype.Ui;
using UnityEngine;

namespace DevourNovelEngine.Prototype.Core.Executors
{
    public sealed class DialogSlideShower : ExecutorComponent<ShowDialogSlideSo>
    {
        [SerializeField] private NovelDialogPanelUi _dialogPanel;


        protected override void ExecuteInherited(ShowDialogSlideSo command)
        {
            _dialogPanel.Say(command);    
        }
    }
}

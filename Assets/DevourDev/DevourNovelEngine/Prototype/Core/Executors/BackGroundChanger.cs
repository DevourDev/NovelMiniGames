using DevourNovelEngine.Prototype.Core.Commands;
using DevourNovelEngine.Prototype.Ui;
using UnityEngine;

namespace DevourNovelEngine.Prototype.Core.Executors
{
    public sealed class BackGroundChanger : ExecutorComponent<ChangeBackGroundSo>
    {
        [SerializeField] private BackGroundManager _bgManager;


        protected override void ExecuteInherited(ChangeBackGroundSo command)
        {
            _bgManager.ChangeBackGround(command.Sprite);
        }
    }
}

using DevourNovelEngine.Prototype.Core.Commands;
using DevourNovelEngine.Prototype.Ui;
using UnityEngine;

namespace DevourNovelEngine.Prototype.Core.Executors
{
    public sealed class CharacterHider : ExecutorComponent<HideCharacterSo>
    {
        [SerializeField] private ItemsOnSceneManager _itemsOnSceneManager;


        protected override void ExecuteInherited(HideCharacterSo command)
        {
            _itemsOnSceneManager.RemoveCharacterSimple(command);
        }
    }
}

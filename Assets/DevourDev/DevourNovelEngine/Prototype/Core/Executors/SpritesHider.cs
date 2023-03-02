using DevourNovelEngine.Prototype.Core.Commands;
using DevourNovelEngine.Prototype.Ui;
using UnityEngine;

namespace DevourNovelEngine.Prototype.Core.Executors
{
    public sealed class SpritesHider : ExecutorComponent<HideSpriteSo>
    {
        [SerializeField] private ItemsOnSceneManager _itemsOnSceneManager;


        protected override void ExecuteInherited(HideSpriteSo command)
        {
            _itemsOnSceneManager.RemoveSpriteSimple(command.Sprite, command.Position);
        }
    }
}

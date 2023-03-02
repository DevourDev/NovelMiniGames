using DevourNovelEngine.Prototype.Core.Commands;
using DevourNovelEngine.Prototype.Ui;
using UnityEngine;

namespace DevourNovelEngine.Prototype.Core.Executors
{
    public sealed class SpritesShower : ExecutorComponent<ShowSpriteSo>
    {
        [SerializeField] private ItemsOnSceneManager _itemsOnSceneManager;


        protected override void ExecuteInherited(ShowSpriteSo command)
        {
            _itemsOnSceneManager.PlaceSpriteSimple(command.Sprite, command.Position);
        }
    }
}

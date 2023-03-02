using DevourNovelEngine.Prototype.Core.Commands;
using DevourNovelEngine.Prototype.Ui;
using UnityEngine;

namespace DevourNovelEngine.Prototype.Core.Executors
{
    public sealed class CharacterShower : ExecutorComponent<ShowCharacterSo>
    {
        [SerializeField] private ItemsOnSceneManager _itemsOnSceneManager;


        protected override void ExecuteInherited(ShowCharacterSo command)
        {
            _itemsOnSceneManager.PlaceCharacterSimple(command);
        }
    }
}

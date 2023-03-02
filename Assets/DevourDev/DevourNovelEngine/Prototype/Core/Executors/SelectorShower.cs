using DevourNovelEngine.Prototype.Core.Commands;
using DevourNovelEngine.Prototype.Ui;
using UnityEngine;

namespace DevourNovelEngine.Prototype.Core.Executors
{
    public sealed class SelectorShower : ExecutorComponent<ShowSelectorSo>
    {
        [SerializeField] private SelectorUi _selectorUi;
        [SerializeField] private StoryLineManager _storyLineManager;


        protected override void ExecuteInherited(ShowSelectorSo command)
        {
            var variants = command.Variants;
            var uiVariants = new ActionVariantUiSetting[variants.Length];

            for (int i = 0; i < variants.Length; i++)
            {
                var variant = variants[i];
                string text = variant.Text.Get();
                void callback() => _storyLineManager.ExecuteAction(variant.Action);
                uiVariants[i] = new ActionVariantUiSetting(text, callback);
            }

            _selectorUi.BuildSelector(command.Title.Text.Get(), uiVariants);
        }
    }
}

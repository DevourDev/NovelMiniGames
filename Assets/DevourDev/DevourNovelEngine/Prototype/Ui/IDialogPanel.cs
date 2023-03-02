using DevourNovelEngine.Prototype.Characters;

namespace DevourNovelEngine.Prototype.Ui
{
    public interface IDialogPanel<TDialogSlideData, TCharacterReference>
        where TDialogSlideData : IDialogSlideData<TCharacterReference>
        where TCharacterReference : ICharacterReference
    {
        void Say(TDialogSlideData slideData);
    }
}

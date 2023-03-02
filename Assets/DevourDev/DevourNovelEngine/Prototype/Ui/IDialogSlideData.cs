using DevourDev.Unity.MultiCulture;

namespace DevourNovelEngine.Prototype.Ui
{
    public interface IDialogSlideData<TCharacterReference>
    {
        TCharacterReference Author { get; }
        MultiCulturalText Text { get; }
    }
}

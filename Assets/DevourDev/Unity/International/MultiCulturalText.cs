namespace DevourDev.Unity.MultiCulture
{
    [System.Serializable]
    public class MultiCulturalText : MultiCulturalItem<string>
    {
        public MultiCulturalText(params Translation<string>[] translations) : base(translations)
        {
        }


        public static new MultiCulturalText Create(string text)
        {
            return new MultiCulturalText(new Translation<string>(International.CurrentCulture, text));
        }

        public static new MultiCulturalText Create(CultureObject culture, string text)
        {
            return new MultiCulturalText(new Translation<string>(culture, text));
        }
    }
}

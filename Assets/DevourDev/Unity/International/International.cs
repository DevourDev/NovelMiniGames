namespace DevourDev.Unity.MultiCulture
{
    public static class International
    {
        private static CultureObject _currentCulture;


        public static CultureObject CurrentCulture => _currentCulture;


        public static void SetCurrentCulture(CultureObject culture)
        {
            _currentCulture = culture;
        }
    }
}

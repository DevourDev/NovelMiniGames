using DevourDev.Unity.MultiCulture;
using UnityEngine;

namespace DevourDev.Unity
{
    [System.Serializable]
    public class MetaInfo
    {
        [SerializeField] private MultiCulturalText _name;
        [SerializeField] private Texture2D _icon;
        [SerializeField] private Texture2D _preview;


        public MetaInfo(MultiCulturalText name, Texture2D icon, Texture2D preview)
        {
            _name = name;
            _icon = icon;
            _preview = preview;
        }


        public MultiCulturalText Name => _name;
        public Texture2D Icon => _icon;
        public Texture2D Preview => _preview;


        public static MetaInfo Create(CultureObject culture, string text)
        {
            MultiCulturalText name = MultiCulturalText.Create(culture, text);
            return new(name, null, null);
        }
    }
}

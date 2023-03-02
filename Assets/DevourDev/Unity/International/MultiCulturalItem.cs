using DevourDev.International;
using UnityEngine;

namespace DevourDev.Unity.MultiCulture
{

    [System.Serializable]
    public class MultiCulturalItem<TItem> : IInternational<TItem, CultureObject>
    {
        [SerializeField] private Translation<TItem>[] _translations;


        public MultiCulturalItem(params Translation<TItem>[] translations)
        {
            _translations = translations;
        }


        public TItem Get(CultureObject culture)
        {
            foreach (var translation in _translations)
            {
                if (translation.Culture == culture)
                    return translation.Item;
            }

            return default;
        }

        public bool TryGet(CultureObject culture, out TItem item)
        {
            foreach (var translation in _translations)
            {
                if (translation.Culture == culture)
                {
                    item = translation.Item;
                    return true;
                }
            }

            item = default;
            return false;
        }

        public TItem Get()
        {
            if (TryGet(International.CurrentCulture, out var item))
                return item;

            if (_translations.Length > 0)
                return _translations[0].Item;

            return default;
        }


        public static MultiCulturalItem<TItem> Create(TItem item)
        {
            return new MultiCulturalItem<TItem>(new Translation<TItem>(International.CurrentCulture, item));
        }

        public static MultiCulturalItem<TItem> Create(CultureObject culture, TItem item)
        {
            return new MultiCulturalItem<TItem>(new Translation<TItem>(culture, item));
        }
    }
}

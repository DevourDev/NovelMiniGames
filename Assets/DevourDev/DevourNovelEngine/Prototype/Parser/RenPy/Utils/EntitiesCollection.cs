using System.Collections.Generic;

namespace DevourNovelEngine.Prototype.Parser.Utils
{
    public abstract class EntitiesCollection<TKey, TValue>
    {
        private readonly Dictionary<TKey, TValue> _items = new();


        public void RegisterEntity(TKey key, TValue value)
        {
            if (_items.TryGetValue(key, out var bogus))
            {
                InitBogusWithReal(bogus, value);
            }
            else
            {
                bogus = value;
                _items.Add(key, value);
            }

            HandleEntityRegistered(bogus);
        }


        public TValue GetEntity(TKey key)
        {
            if (!_items.TryGetValue(key, out var v))
            {
                v = CreateBogus(key);
                _items.Add(key, v); // no event raises
            }

            return v;
        }


        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
            => _items.GetEnumerator();

        public TValue[] ToArray()
        {
            var values = _items.Values;
            TValue[] arr = new TValue[values.Count];
            values.CopyTo(arr, 0);
            return arr;
        }


        protected virtual void HandleEntityRegistered(TValue entity) { }

        protected abstract TValue CreateBogus(TKey key);
        protected abstract void InitBogusWithReal(TValue bogus, TValue real);
    }
}

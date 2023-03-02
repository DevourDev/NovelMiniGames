namespace DevourDev.Collections.Generic
{
    public sealed class KeyAndIndexBucket<TKey, TValue>
    {
        internal KeyAndIndexDictionary<TKey, TValue> _parent;
        internal TKey _key;
        internal int _index;
        internal TValue _value;


        internal KeyAndIndexBucket(KeyAndIndexDictionary<TKey, TValue> parent,
            TKey key, int index, TValue value)
        {
            _parent = parent;
            _key = key;
            _index = index;
            _value = value;
        }


        public TKey Key => _key;
        public int Index => _index;
        public TValue Value => _value;
    }
}

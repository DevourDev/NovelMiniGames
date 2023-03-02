using System;
using System.Collections.Generic;

namespace DevourDev.Collections.Generic
{
    public static class KeyAndIndexDictionaryInternalsExposer
    {
        public static void SetValue<TKey, TValue>(KeyAndIndexBucket<TKey, TValue> bucket, TValue newValue)
        {
            bucket._value = newValue;
        }
    }


    public sealed class KeyAndIndexDictionary<TKey, TValue>
    {
        private readonly Dictionary<TKey, KeyAndIndexBucket<TKey, TValue>> _byKey = new();
        private readonly List<KeyAndIndexBucket<TKey, TValue>> _byIndex = new();


        public KeyAndIndexBucket<TKey, TValue> GetByKey(TKey key)
        {
            return _byKey[key];
        }

        public KeyAndIndexBucket<TKey, TValue> GetByIndex(int index)
        {
            return _byIndex[index];
        }


        public bool TryGetByKey(TKey key, out KeyAndIndexBucket<TKey, TValue> handle)
        {
            return _byKey.TryGetValue(key, out handle);
        }

        public bool TryGetByIndex(int index, out KeyAndIndexBucket<TKey, TValue> handle)
        {
            if (_byIndex.Count < index)
            {
                handle = _byIndex[index];
                return true;
            }

            handle = null;
            return false;
        }

        public bool TryAdd(TKey key, TValue value, out KeyAndIndexBucket<TKey, TValue> handle)
        {
            if (_byKey.ContainsKey(key))
            {
                handle = null;
                return false;
            }

            handle = AddInternal(key, value);
            return true;
        }

        private KeyAndIndexBucket<TKey, TValue> AddInternal(TKey key, TValue value)
        {
            int index = _byIndex.Count;
            var bucket = new KeyAndIndexBucket<TKey, TValue>(this, key, index, value);
            _byKey.Add(key, bucket);
            _byIndex.Add(bucket);
            return bucket;
        }

        public TValue[] GetValues()
        {
            var buckets = _byIndex;
            var length = buckets.Count;
            TValue[] arr = new TValue[length];

            for (int i = 0; i < length; i++)
            {
                arr[i] = buckets[i].Value;
            }

            return arr;
        }
    }
}

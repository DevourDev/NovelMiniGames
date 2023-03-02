using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace DevourDev.Unity.Utils
{
    public sealed class ExposableList<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable,
        IList, ICollection, IReadOnlyList<T>, IReadOnlyCollection<T>
    {
        private readonly List<T> _list = new();

        private T[] _cachedInternalArray = Array.Empty<T>();

        public int Count => ((ICollection<T>)_list).Count;

        public bool IsReadOnly => ((ICollection<T>)_list).IsReadOnly;

        public bool IsFixedSize => ((IList)_list).IsFixedSize;

        public bool IsSynchronized => ((ICollection)_list).IsSynchronized;

        public object SyncRoot => ((ICollection)_list).SyncRoot;

        object IList.this[int index] { get => ((IList)_list)[index]; set => ((IList)_list)[index] = value; }
        public T this[int index] { get => ((IList<T>)_list)[index]; set => ((IList<T>)_list)[index] = value; }

        public List<T> GetInternalList()
        {
            return _list;
        }

        public T[] GetInternalArray()
        {
            if(_cachedInternalArray.Length != _list.Capacity)
            {
                var items = typeof(List<T>).GetField("_items", BindingFlags.Instance | BindingFlags.NonPublic);
                _cachedInternalArray = (T[])items.GetValue(_list);
            }

            return _cachedInternalArray;
        }

        public ReadOnlyMemory<T> AsMemory()
        {
            return GetInternalArray().AsMemory();
        }

        public int IndexOf(T item)
        {
            return ((IList<T>)_list).IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            ((IList<T>)_list).Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            ((IList<T>)_list).RemoveAt(index);
        }

        public void Add(T item)
        {
            ((ICollection<T>)_list).Add(item);
        }

        public void Clear()
        {
            ((ICollection<T>)_list).Clear();
        }

        public bool Contains(T item)
        {
            return ((ICollection<T>)_list).Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            ((ICollection<T>)_list).CopyTo(array, arrayIndex);
        }

        public bool Remove(T item)
        {
            return ((ICollection<T>)_list).Remove(item);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>)_list).GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return ((System.Collections.IEnumerable)_list).GetEnumerator();
        }

        public int Add(object value)
        {
            return ((IList)_list).Add(value);
        }

        public bool Contains(object value)
        {
            return ((IList)_list).Contains(value);
        }

        public int IndexOf(object value)
        {
            return ((IList)_list).IndexOf(value);
        }

        public void Insert(int index, object value)
        {
            ((IList)_list).Insert(index, value);
        }

        public void Remove(object value)
        {
            ((IList)_list).Remove(value);
        }

        public void CopyTo(Array array, int index)
        {
            ((ICollection)_list).CopyTo(array, index);
        }
    }

}
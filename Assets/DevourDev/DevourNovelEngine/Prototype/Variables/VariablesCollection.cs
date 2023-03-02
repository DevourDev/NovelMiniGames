using System.Collections.Generic;

namespace DevourNovelEngine.Prototype.Variables
{
    public sealed class VariablesCollection
    {
        private readonly Dictionary<IVariableKey, IVariableData<IVariableKey>> _dict;


        public VariablesCollection()
        {
            _dict = new();
        }


        public bool TryGetVariableValue<T>(IVariableKey<T> key, out T value)
        {
            if (_dict.TryGetValue(key, out var rawData))
            {
                if (rawData is IVariableData<IVariableKey<T>, T> safeData)
                {
                    value = safeData.Value;
                    return true;
                }
            }

            value = default!;
            return false;
        }


        public bool TryGetVariableData<TKey, TValue>(TKey key,
            out IVariableData<TKey, TValue> variableData) where TKey : IVariableKey<TValue>
        {
            if (_dict.TryGetValue(key, out var rawData))
            {
                if (rawData is IVariableData<TKey, TValue> safeData)
                {
                    variableData = safeData;
                    return true;
                }
            }

            variableData = default!;
            return false;
        }

        public TData GetOrCreateAndAddVariableData<TKey, TValue, TData>(TKey key)
            where TKey : IVariableKey<TValue>
            where TData : IVariableData<TKey, TValue>, new()
        {
            if(!TryGetVariableData<TKey, TValue>(key, out var variableData))
            {
                variableData = new TData();
                _dict.Add(key, (IVariableData<IVariableKey>)variableData);
            }

            return (TData)variableData;
        }


        public IEnumerator<IVariableData<IVariableKey>> GetEnumerator()
        {
            return _dict.Values.GetEnumerator();
        }
    }
}
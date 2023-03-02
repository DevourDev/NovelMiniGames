namespace DevourNovelEngine.Prototype.Variables
{
    public interface IVariableData<TKey> where TKey : IVariableKey
    {
        TKey Key { get; }
    }

    public interface IVariableData<TKey, TValue> : IVariableData<TKey>
        where TKey : IVariableKey<TValue>
    {
        TValue Value { get; }
    }
}
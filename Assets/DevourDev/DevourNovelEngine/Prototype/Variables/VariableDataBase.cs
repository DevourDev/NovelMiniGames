namespace DevourNovelEngine.Prototype.Variables
{
    public abstract class VariableDataBase<TKey, TValue> : IVariableData<TKey, TValue>
        where TKey : IVariableKey<TValue>
    {
        private readonly TKey _key;
        private TValue _value;


        public TKey Key => _key;
        public TValue Value { get => _value; protected set => _value = value; }
    }
}
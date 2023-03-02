using UnityEngine;

namespace DevourNovelEngine.Prototype.Parser.RenPy.Converters
{
    public abstract class SoConverterBase<TFrom, TTo> : IConverter<TFrom, TTo>
        where TTo : UnityEngine.ScriptableObject
    {
        public TTo Convert(TFrom from)
        {
            var so = ScriptableObject.CreateInstance<TTo>();
            InitSo(so, from);
            return so;
        }


        protected abstract void InitSo(TTo so, TFrom from);
    }
}

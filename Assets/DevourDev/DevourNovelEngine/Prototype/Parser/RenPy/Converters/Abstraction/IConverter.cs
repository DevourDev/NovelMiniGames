using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Rendering;

namespace DevourNovelEngine.Prototype.Parser.RenPy.Converters
{
    public interface IConverter
    {
        Type FromType { get; }
        Type ToType { get; }


        object ConvertObject(object from);
    }


    public interface IConverter<TFrom, TTo> : IConverter
    {
        Type IConverter.FromType => typeof(TFrom);
        Type IConverter.ToType => typeof(TTo);


        object IConverter.ConvertObject(object from) => Convert((TFrom)from);


        TTo Convert(TFrom from);
    }
}

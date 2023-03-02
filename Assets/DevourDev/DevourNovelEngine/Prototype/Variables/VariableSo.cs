using System;
using DevourDev.Unity;
using DevourDev.Unity.ScriptableObjects;
using UnityEngine;

namespace DevourNovelEngine.Prototype.Variables
{
    public abstract class VariableSo : SoDatabaseElement, IVariableKey
    {
        [SerializeField] private MetaInfo _metaInfo;


        public MetaInfo MetaInfo => _metaInfo;

        internal protected abstract System.Type VariableDataType { get; }
    }

    public abstract class VariableSo<TValue> : VariableSo, IVariableKey<TValue>
    {
        protected internal sealed override Type VariableDataType => typeof(TValue);
    }
}
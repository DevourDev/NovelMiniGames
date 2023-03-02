using System;
using UnityEngine;

namespace DevourDev.Unity.Utils
{
    public static class GameObjectExtentions
    {
        private static readonly ExposableList<Component> _componentsBuffer = new();


        public static ReadOnlyMemory<Component> GetComponentsNonAlloc(this GameObject go, Type type)
        {
            _componentsBuffer.Clear();
            go.GetComponents(type, _componentsBuffer.GetInternalList());
            return _componentsBuffer.AsMemory();
        }
    }

}
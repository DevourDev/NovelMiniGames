using System;
using System.Collections.Generic;
using UnityEngine;

namespace DevourDev.Unity.Helpers
{
    public sealed class CachingComponents : MonoBehaviour
    {
        [SerializeField] private bool _addAllComponents;

        private Dictionary<Type, List<Component>> _items;


        private void Awake()
        {
            if (_addAllComponents)
            {
                var allComps = gameObject.GetComponents(typeof(Component));
                _items = new Dictionary<Type, List<Component>>(allComps.Length);

                foreach (var comp in allComps)
                {
                    CacheComponent(comp);
                }
            }
            else
            {
                _items = new();
            }
        }

        private void CacheComponent(Component comp)
        {
            if (!_items.TryGetValue(comp.GetType(), out var compsList))
            {
                compsList = new List<Component>();
                _items.Add(comp.GetType(), compsList); compsList.Add(comp);
            }

            compsList.Add(comp);
        }


        public new bool TryGetComponent<T>(out T comp) where T : UnityEngine.Component
        {
            if (TryGetComponents<T>(out var comps))
            {
                comp = comps[0];
                return true;
            }

            comp = null;
            return false;
        }

        public bool TryGetComponents<T>(out IReadOnlyList<T> comps) where T : UnityEngine.Component
        {
            if (!_items.TryGetValue(typeof(T), out var compsRaw))
            {
                compsRaw = new();
                gameObject.GetComponents(typeof(T), compsRaw);
                _items.Add(typeof(T), compsRaw);
            }

            comps = (IReadOnlyList<T>)compsRaw;
            return comps.Count > 0;
        }
    }
}

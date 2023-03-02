using UnityEngine;

namespace Game.Stealth
{
    public sealed class InitablesIniter : MonoBehaviour
    {
        [System.Serializable]
        private enum WhenXDD
        {
            OnAwake,
            OnStart,
            FromScript
        }

        [SerializeField] private InitableComponent[] _components;
#if UNITY_EDITOR
        [SerializeField] private bool _findAll;
#endif
        [SerializeField] private WhenXDD _whenInit;


#if UNITY_EDITOR
        private void OnValidate()
        {
            if (_findAll)
            {
                _findAll = false;
                _components = FindObjectsOfType<InitableComponent>(true);
                UnityEditor.EditorUtility.SetDirty(this);
            }
        }
#endif

        private void Awake()
        {
            if (_whenInit == WhenXDD.OnAwake)
                InitComponents();
        }

        private void Start()
        {
            if (_whenInit == WhenXDD.OnStart)
                InitComponents();
        }

        public void InitComponents()
        {
            foreach (var cmp in _components)
            {
                cmp.Init();
            }
        }
    }
}

using DevourDev.Patterns.Command;
using DevourDev.Unity.Utils;
using UnityEngine;

namespace DevourNovelEngine.Prototype.Core.Executors
{
    public sealed class ExecutorsImporter : MonoBehaviour
    {
        [SerializeField] private ExecutorsCompositeComponent _composite;


        private void Awake()
        {
            var executors = gameObject.GetComponentsNonAlloc(typeof(ICommandExecutor));

            Debug.Log($"{executors.Length} executors found!");

            var span = executors.Span;
            var length = span.Length;

            for (int i = 0; i < length; i++)
            {
                var executor = (ICommandExecutor)span[i];
                _composite.AddExecutor(executor.CommandType, executor);
            }
        }
    }


}

using System.Runtime.InteropServices;
using DevourDev.Patterns.Command;
using UnityEngine;

namespace DevourNovelEngine.Prototype.Core.Executors
{
    public abstract class ExecutorComponent<TCommand> : MonoBehaviour, ICommandExecutor<TCommand>
    {
        public void Execute(TCommand command)
        {
            ExecuteInherited(command);
        }

        protected abstract void ExecuteInherited(TCommand command);
    }
}

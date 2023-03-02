using System;
using DevourDev.Patterns.Command;
using UnityEngine;

namespace DevourNovelEngine.Prototype.Core.Executors
{
    public class ExecutorsCompositeComponent : MonoBehaviour, IExecutorsComposite
    {
        private readonly ExecutorsComposite _internalComposite = new();


        Type ICommandExecutor.CommandType => throw new NotSupportedException("unable to request command type from composite");


        public void AddExecutor<TCommand>(ICommandExecutor<TCommand> executor)
        {
            _internalComposite.AddExecutor(executor);
        }

        public void AddExecutor(Type commandType, ICommandExecutor executor)
        {
            _internalComposite.AddExecutor(commandType, executor);
        }

        public void Execute(object command)
        {
            _internalComposite.Execute(command);
        }

        public void ExecuteExact<TCommand>(TCommand command)
        {
            _internalComposite.ExecuteExact(command);
        }

        public void ExecuteExact(Type commandType, object command)
        {
            _internalComposite.ExecuteExact(commandType, command);
        }
    }
}

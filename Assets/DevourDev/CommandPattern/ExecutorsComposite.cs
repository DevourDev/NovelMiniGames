using System;
using System.Collections.Generic;

namespace DevourDev.Patterns.Command
{
    public class ExecutorsComposite : IExecutorsComposite
    {
        private readonly Dictionary<Type, ICommandExecutor> _executors;


        public ExecutorsComposite()
        {
            _executors = new();
        }


        public bool OverrideOverlapingExecutors { get; set; }

        Type ICommandExecutor.CommandType => throw new NotSupportedException("unable to request command type from composite");


        public void Execute(object command)
        {
            ExecuteExact(command.GetType(), command);
        }

        public void ExecuteExact<TCommand>(TCommand command)
        {
            ExecuteExact(typeof(TCommand), command);
        }

        public void ExecuteExact(Type commandType, object command)
        {
            if (_executors.TryGetValue(commandType, out var executor))
            {
                executor.Execute(command);
                return;
            }

            throw new KeyNotFoundException($"executor for command of type {commandType} was not found");
        }


        public void AddExecutor<TCommand>(ICommandExecutor<TCommand> executor)
        {
            AddExecutor(typeof(TCommand), executor);
        }

        public void AddExecutor(Type commandType, ICommandExecutor executor)
        {
            if (!_executors.TryAdd(commandType, executor))
            {
                if (OverrideOverlapingExecutors)
                    _executors[commandType] = executor;
                else
                    throw new System.ArgumentException($"key '{commandType}' already exists in collection");
            }
        }
    }
}

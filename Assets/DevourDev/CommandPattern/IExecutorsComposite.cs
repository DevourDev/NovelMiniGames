using System;

namespace DevourDev.Patterns.Command
{
    public interface IExecutorsComposite : ICommandExecutor
    {
        void ExecuteExact<TCommand>(TCommand command);
        void ExecuteExact(Type commandType, object command);

        void AddExecutor<TCommand>(ICommandExecutor<TCommand> executor);
        void AddExecutor(Type commandType, ICommandExecutor executor);
    }
}

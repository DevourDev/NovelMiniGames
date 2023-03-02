namespace DevourDev.Patterns.Command
{
    public interface ICommandExecutor<TCommand> : ICommandExecutor
    {
        System.Type ICommandExecutor.CommandType => typeof(TCommand);

        void ICommandExecutor.Execute(object command) => Execute((TCommand)command);

        void Execute(TCommand command);

    }

    public interface ICommandExecutor
    {
        System.Type CommandType { get; }

        void Execute(object command);
    }
}

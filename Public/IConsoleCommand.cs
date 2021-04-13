public interface IConsoleCommand
{
    string Name { get; }
    void Execute(ICommandArguments arguments);
    string GetHelp();
}

public interface IConsoleCommand
{
    string Name { get; }
    IDeveloperConsole DeveloperConsole { get; set; }
    void Execute(ICommandArguments arguments);
    string GetHelp(ICommandArguments arguments);
}

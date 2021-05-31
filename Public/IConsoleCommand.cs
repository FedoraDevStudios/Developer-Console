namespace FedoraDev.DeveloperConsole
{
    public interface IConsoleCommand
    {
        string Name { get; }
        string Usage { get; }
        IDeveloperConsole DeveloperConsole { get; set; }
        void Execute(ICommandArguments arguments);
        string[] GetHelp(ICommandArguments arguments);
    }
}
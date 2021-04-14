public class ClearCommand : IConsoleCommand
{
	IDeveloperConsole _developerConsole;

	public string Name => "clear";
	public string Usage => "clear";

	public IDeveloperConsole DeveloperConsole { get => _developerConsole; set => _developerConsole = value; }
	public void Execute(ICommandArguments arguments) => _developerConsole.ClearLog();
	public string[] GetHelp(ICommandArguments arguments) => new string[] { "Clears the console's log." };
}

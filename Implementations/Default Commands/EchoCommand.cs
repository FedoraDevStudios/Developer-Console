public class EchoCommand : IConsoleCommand
{
	public string Name => "echo";
	public IDeveloperConsole DeveloperConsole { get => _developerConsole; set => _developerConsole = value; }

	IDeveloperConsole _developerConsole;

	public void Execute(ICommandArguments arguments)
	{
		DeveloperConsole.PushMessage(arguments.TextEntered.Substring(arguments.CommandName.Length));
	}

	public string GetHelp(ICommandArguments arguments)
	{
		return "Prints the text that follows the command to the console.";
	}
}

using System.Collections.Generic;

public class SpillCommand : IConsoleCommand
{
	public string Name => "spill";
	public string Usage => "spill {contents}";

	IDeveloperConsole _developerConsole;
	public IDeveloperConsole DeveloperConsole { get => _developerConsole; set => _developerConsole = value; }

	public void Execute(ICommandArguments arguments)
	{
		if (arguments.ArgumentQuantity == 0)
		{
			_developerConsole.PushMessages(GetHelp(arguments));
			return;
		}

		char[] alphabet = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };

		_developerConsole.PushMessage($"Text Entered: {arguments.TextEntered.Substring(arguments.CommandName.Length + 1)}");
		_developerConsole.PushMessage($"Command: {arguments.GetArgument(0)}");
		_developerConsole.PushMessage($"Arguments: {arguments.ArgumentQuantity - 1}");

		for (int i = 1; i < arguments.ArgumentQuantity; i++)
			_developerConsole.PushMessage($"        {i}: {arguments.GetArgument(i)}");

		List<char> assignedFlags = new List<char>();

		foreach (char flag in alphabet)
		{
			string flagValue = arguments.GetFlag(flag);
			if (flagValue != null)
				assignedFlags.Add(flag);
		}

		_developerConsole.PushMessage($"Flags: {assignedFlags.Count}");

		for (int i = 0; i < assignedFlags.Count; i++)
			_developerConsole.PushMessage($"        {assignedFlags[i]}: {arguments.GetFlag(assignedFlags[i])}");
	}

	public string[] GetHelp(ICommandArguments arguments)
	{
		return new string[] {
			"Spills the command argument object contents of the given text.",
			"Usage:",
			Usage
		};
	}
}

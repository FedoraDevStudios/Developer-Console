using System.Collections.Generic;

public class DefaultDeveloperConsole : IDeveloperConsole
{
	List<IConsoleCommand> _commands = new List<IConsoleCommand>();

	public bool RegisterCommand(IConsoleCommand command)
	{
		_commands.Add(command);
		return true;
	}

	public void ProcessCommand(string input)
	{
		//inventory add storable inventory -v -h=yowhat
		string[] inputParts = input.Split(' ');
		List<string> arguments = new List<string>();
		Dictionary<string, string> flags = new Dictionary<string, string>();

		for (int i = 0; i < inputParts.Length; i++)
		{
			string part = inputParts[i];

			if (part.StartsWith("-"))
			{
				//This is a flag
				string[] flagParts = part.Split('=');
				if (flagParts.Length > 1)
					flags.Add(flagParts[0], flagParts[1]);
				else
					flags.Add(flagParts[0], "true");
			}
			else
			{
				//This is an argument
				arguments.Add(part);
			}
		}
	}

	public void PushMessage(string message)
	{
		//
	}

	public void PushMessages(string[] messages)
	{
		//
	}
}

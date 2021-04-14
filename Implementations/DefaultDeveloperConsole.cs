using System;
using System.Collections.Generic;

public class DefaultDeveloperConsole : IDeveloperConsole
{
	List<IConsoleCommand> _commands = new List<IConsoleCommand>();

	public string MessageLog => _messageLog;

	string _messageLog;
	int _indent = 0;

	public bool RegisterCommand(IConsoleCommand command)
	{
		_commands.Add(command);
		return true;
	}

	public void ProcessCommand(string input)
	{
		PushMessage($"> {input}");
		_indent += 4;
		//inventory add storable inventory -v -h=yowhat
		string[] inputParts = input.Split(' ');
		List<string> arguments = new List<string>();
		Dictionary<string, string> flags = new Dictionary<string, string>();

		for (int i = 1; i < inputParts.Length; i++)
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

		DefaultCommandArguments commandArguments = new DefaultCommandArguments(inputParts[0], arguments.ToArray(), flags);

		for (int i = 0; i < _commands.Count; i++)
		{
			if (commandArguments.CommandName == _commands[i].Name)
			{
				try
				{
					_commands[i].Execute(commandArguments);
					break;
				}
				catch (Exception e)
				{
					string[] messages = new string[]
					{
						$"There was an error while running the command.",
						$"'{commandArguments.CommandName}'",
						$"Produced:",
						$"{e.Message}",
						$"{e.StackTrace}"
					};

					PushMessages(messages);
					break;
				}
			}
		}

		_indent -= 4;
	}

	public void PushMessage(string message)
	{
		string indentation = new string(' ', _indent);
		_messageLog += $"\n{indentation}{message}";
	}

	public void PushMessages(string[] messages)
	{
		foreach (string message in messages)
			PushMessage(message);
	}

	public void ClearLog()
	{
		_messageLog = string.Empty;
	}
}

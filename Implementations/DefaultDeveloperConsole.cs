using System;
using System.Collections.Generic;
using UnityEngine;

public enum OverrideRule
{
	Ignore,
	Replace,
	Rename
}

public class DefaultDeveloperConsole : IDeveloperConsole
{
	[SerializeField] OverrideRule _overrideRule = OverrideRule.Ignore;

	Dictionary<string, IConsoleCommand> _commands = new Dictionary<string, IConsoleCommand>();

	public string MessageLog => _messageLog;

	string _messageLog;
	int _indent = 0;

	public bool RegisterCommand(IConsoleCommand command)
	{
		if (_commands == null)
			_commands = new Dictionary<string, IConsoleCommand>();

		foreach (KeyValuePair<string, IConsoleCommand> consoleCommand in _commands)
		{
			if (command.Name == consoleCommand.Key)
			{
				PushMessage($"Command with name '{command.Name}' is already registered.");

				switch (_overrideRule)
				{
					case OverrideRule.Ignore:
						PushMessage($"Command will be ignored.");
						return true;

					case OverrideRule.Replace:
						PushMessage($"Command will be overridden.");
						_commands[command.Name] = command;
						return true;

					case OverrideRule.Rename:
						PushMessage($"Command will be renamed.");
						int increment = 1;
						string newCommandName = $"{command.Name}_{increment}";
						while (_commands.ContainsKey(newCommandName))
							newCommandName = $"{command.Name}_{++increment}";

						_commands.Add(newCommandName, command);
						PushMessage($"Command will be named '{newCommandName}'");
						return true;
				}
			}
		}

		_commands.Add(command.Name, command);
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

		string commandName = inputParts[0];
		bool printHelp = false;

		if (commandName == "help")
		{
			if (arguments.Count == 0)
			{
				PushMessage("To use a command, use the following syntax:");
				PushMessage("{command name} [arguments|flags]");
				PushMessage("Available Commands:");

				foreach (KeyValuePair<string, IConsoleCommand> consoleCommand in _commands)
					PushMessage($"  {consoleCommand.Key}");

				PushMessage(string.Empty);
				_indent -= 4;
				return;
			}

			printHelp = true;
			commandName = arguments[0];
			arguments.RemoveAt(0);
		}

		DefaultCommandArguments commandArguments = new DefaultCommandArguments(input, commandName, arguments.ToArray(), flags);


		foreach (KeyValuePair<string, IConsoleCommand> consoleCommand in _commands)
		{
			if (commandArguments.CommandName == consoleCommand.Key)
			{
				try
				{
					if (printHelp)
					{
						string helpText = consoleCommand.Value.GetHelp(commandArguments);
						PushMessage(helpText);
						break;
					}

					consoleCommand.Value.Execute(commandArguments);
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

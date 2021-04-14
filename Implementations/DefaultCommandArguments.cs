using System.Collections.Generic;

public struct DefaultCommandArguments : ICommandArguments
{
	string _commandName;
	string[] _arguments;
	Dictionary<string, string> _flags;

	public DefaultCommandArguments(string commandName, string[] arguments, Dictionary<string, string> flags)
	{
		_commandName = commandName;
		_arguments = arguments;
		_flags = flags;
	}

	public string CommandName => throw new System.NotImplementedException();
	public int ArgumentQuantity => throw new System.NotImplementedException();

	public string GetArgument(int index) => throw new System.NotImplementedException();
	public string GetFlag(string flagName) => throw new System.NotImplementedException();
}

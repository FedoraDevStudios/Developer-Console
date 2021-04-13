public interface ICommandArguments
{
    string CommandName { get; }
    int ArgumentQuantity { get; }

    string GetArgument(int index);
    string GetFlag(string flagName);
}

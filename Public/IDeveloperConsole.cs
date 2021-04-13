public interface IDeveloperConsole
{
    bool RegisterCommand(IConsoleCommand command);
    void ProcessCommand(string input);
    void PushMessage(string message);
    void PushMessages(string[] messages);
}

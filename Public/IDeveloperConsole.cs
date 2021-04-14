public interface IDeveloperConsole
{
    string MessageLog { get; }

    bool RegisterCommand(IConsoleCommand command);
    void ProcessCommand(string input);
    void PushMessage(string message);
    void PushMessages(string[] messages);
    void ClearLog();
}

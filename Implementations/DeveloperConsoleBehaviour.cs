using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeveloperConsoleBehaviour : SerializedMonoBehaviour, IDeveloperConsole
{
	[SerializeField, HideLabel, BoxGroup("Console")] IDeveloperConsole _console;
	[SerializeField] TMP_Text _logField;
	[SerializeField] Scrollbar _scrollbar;

	private void Awake()
	{
		UpdateConsoleWindow();
	}

	void UpdateConsoleWindow()
	{
		_logField.text = _console.MessageLog;
		_scrollbar.value = 0;
	}

	public string MessageLog => _console.MessageLog;

	public bool RegisterCommand(IConsoleCommand command) => _console.RegisterCommand(command);

	public void ProcessCommand(string input)
	{
		_console.ProcessCommand(input);
		UpdateConsoleWindow();
	}

	public void PushMessage(string message)
	{
		_console.PushMessage(message);
		UpdateConsoleWindow();
	}

	public void PushMessages(string[] messages)
	{
		_console.PushMessages(messages);
		UpdateConsoleWindow();
	}

	public void ClearLog()
	{
		_console.ClearLog();
		UpdateConsoleWindow();
	}
}

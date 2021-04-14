using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FedoraDev.DeveloperConsole.Implementations
{
	public class DeveloperConsoleBehaviour : SerializedMonoBehaviour, IDeveloperConsole
	{
		[SerializeField, HideLabel, BoxGroup("Console")] IDeveloperConsole _console;
		[SerializeField] TMP_Text _logField;
		[SerializeField] Scrollbar _scrollbar;

		private void Start()
		{
			UpdateConsoleWindow();
		}

		void UpdateConsoleWindow()
		{
			_logField.text = _console.MessageLog;
			_scrollbar.value = 0;
		}

		public string MessageLog => _console.MessageLog;

		public void RegisterCommand(IConsoleCommand command)
		{
			_console.RegisterCommand(command);
			command.DeveloperConsole = this;
		}

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
}
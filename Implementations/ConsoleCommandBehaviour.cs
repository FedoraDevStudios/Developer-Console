using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(DeveloperConsoleBehaviour))]
public class ConsoleCommandBehaviour : SerializedMonoBehaviour
{
	[SerializeField, HideLabel, BoxGroup("Commands")] IConsoleCommand[] _commands = new IConsoleCommand[0];

	DeveloperConsoleBehaviour _developerConsoleBehaviour;

	private void Awake()
	{
		_developerConsoleBehaviour = FindObjectOfType<DeveloperConsoleBehaviour>();

		foreach (IConsoleCommand command in _commands)
			_developerConsoleBehaviour.RegisterCommand(command);
	}
}

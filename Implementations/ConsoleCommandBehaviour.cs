using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(DeveloperConsoleBehaviour))]
public class ConsoleCommandBehaviour : SerializedMonoBehaviour
{
	[SerializeField, HideLabel, BoxGroup("Commands")] IConsoleCommand[] _commands = new IConsoleCommand[0];
}

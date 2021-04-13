using System.Collections.Generic;
using UnityEngine;

public class DeveloperConsoleBehaviour : MonoBehaviour, IDeveloperConsole
{
	//

	public bool RegisterCommand(IConsoleCommand command)
	{
		return true;
	}

	public void ProcessCommand(string input)
	{
		//
	}

	public void PushMessage(string message)
	{
		//
	}

	public void PushMessages(string[] messages)
	{
		//
	}
}

using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace FedoraDev.DeveloperConsole.Examples
{
	[RequireComponent(typeof(TMP_InputField))]
	public class ConsoleInputBehaviour : SerializedMonoBehaviour
	{
		[SerializeField] IDeveloperConsole _developerConsole;

		TMP_InputField _inputField;
		bool _isFocused = false;

		private void Awake()
		{
			_inputField = GetComponent<TMP_InputField>();
		}

		private void Update()
		{
			if (_developerConsole == null)
				return;

			if (_isFocused && Input.GetKeyDown(KeyCode.Return))
			{
				_developerConsole.ProcessCommand(_inputField.text);
				_inputField.text = string.Empty;
				_inputField.OnPointerClick(new PointerEventData(FindObjectOfType<EventSystem>()));
			}
			else
				_isFocused = _inputField.isFocused;
		}
	}
}
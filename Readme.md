# Developer Console

A lightweight and extendible developer console system for Unity.

## Installation
This project uses [Odin Inspector](https://assetstore.unity.com/packages/tools/utilities/odin-inspector-and-serializer-89041), which I cannot redistribute. If you don't own Odin Inspector, I would highly recommend purchasing it otherwise you won't be able to serialize interface instances as members which completely breaks this solution.

#### Package Manager
In Unity's Package Manager, you can add the repo as a git package. I have been able to get this to work by using the `https` method, however you can't grab a specific version this way and will always be stuck using the latest release. In Github, select `Code > HTTPS` and click on the clip board. Back in Unity, open Package Manager and hit the plus button and select `Add package from git url`. Paste the link there and the package will be added automatically. If you do want a specific version, you can append `#version` to the end of the link. e.g. `#1.0.2`.

#### UPM Upgrade
If you are content keeping the latest version, you can add [UnityGitPackageUpdater](https://github.com/QuantumCalzone/UnityGitPackageUpdater)  via `https://github.com/QuantumCalzone/UnityGitPackageUpdater.git#upm` as well. This gives Unity an extra window under `Window > Package Updater` where you can easily update any git packages in your project.

#### Manual Installation
Unity's Package Manager seems to have issues with git repositories, however this can be added as a dependency to your Unity project manually.

You just need to add a reference to this repo to your project's `Packages/manifest.json` file. Be sure to switch `[version]` with whichever release you would prefer, e.g. `.git#1.0.2`.

```js
{
    "dependencies": {
        ...,
        "com.fedoradev.developerconsole": "https://github.com/FedoraDevStudios/Developer-Console.git#[version]"
    }
}
```

#### Manual Upgrade
After installing manually, you have to change both `Packages/manifest.json` and `Packages/packages-lock.json`. In the former, simply update the dependency with the version you wish to pull. In the lock file, you need to remove the entry for the package. This entry is a few lines long and everything needs to be deleted, including the curly braces. After this is successfully completed, moving back to Unity will force the application to download the desired version.

## Usage
### Add to Scene
In the package, there is a prefab in the example provided that comes with everything set up. Add this prefab to a canvas and modify it to suit your needs. I would recommend creating a prefab variant from the provided one for ease of use later. You will also need to add a script to hide and show the console window as this is not preconfigured.

### Create a Command
If you're using `asmdef` files, you should only need to reference `_FedoraDev.DeveloperConsole`. You can reference `_FedoraDev.DeveloperConsole.Implementations` as well, however this will create hard dependencies and isn't the intended way to use this system.

For a quick example on how to create your own command, you can take a look at some default implementations provided. These commands are not complex and are easy to follow. You can create a copy of one of these commands to start. Let's review one here.

```c#
public class EchoCommand : IConsoleCommand
{
	public string Name => "echo";
	public string Usage => "echo {text to display}";
	public IDeveloperConsole DeveloperConsole { get => _developerConsole; set => _developerConsole = value; }

	IDeveloperConsole _developerConsole;

	public void Execute(ICommandArguments arguments)
	{
		DeveloperConsole.PushMessage(arguments.TextEntered.Substring(arguments.CommandName.Length));
	}

	public string[] GetHelp(ICommandArguments arguments)
	{
		return "Prints the text that follows the command to the console.";
	}
}
```

##### Name
```c#
public string Name => "echo";
```
This tells the console how to refer to this command. When a user types 'echo' at the beginning of the command string, the console will refer to this command.

##### Usage
```c#
public string Usage => "echo {text to display}";
```
This string displays when the command is listed by the console's help command. This is an easy way to tell the user how your command expects its format.

##### DeveloperConsole
```c#
public IDeveloperConsole DeveloperConsole { get => _developerConsole; set => _developerConsole = value; }
IDeveloperConsole _developerConsole;
```
We hold a reference to the Developer Console instance. This property gets assigned when the command is loaded into the console.

##### Execute
```c#
public void Execute(ICommandArguments arguments)
{
	DeveloperConsole.PushMessage(arguments.TextEntered.Substring(arguments.CommandName.Length));
}
```
Here is the meat and potatoes of the command. `ICommandArguments` comes with a lot of useful parts for your command so you don't have to parse the user input manually with each command. More on the interface below.

##### GetHelp
```c#
public string[] GetHelp(ICommandArguments arguments)
{
	return new string[] { "Prints the text that follows the command to the console." };
}
```
When a user types `help echo`, this gives the console the proper strings to print out. This returns a list of strings so that formatting is consistent within the console. In the default implementation, the console indents some parts of the command output for readability.

### Add a Command to the Console
Once you have a command created that implements `IConsoleCommand`, you can add the command to the list on the `ConsoleCommandBehaviour` component located on the Developer Console Game Object. Next time you run the game, your command will be loaded in with the rest. To test, you can use the builtin `help` command to get a list of the available commands.

## Details
### Using Command Arguments
The Command Arguments object contains a few useful nuggets of information for your command. For the below examples, assume the input received at the prompt is as follows:

##### Example
`inventory insert 13324678 -s=5 player -v`

##### TextEntered
```c#
string TextEntered { Get; }
```
This is the raw text that the user entered. In most cases, you won't need this information.

##### CommandName
```c#
string CommandName { get; }
```
This is the command name received. In the example, this would be `inventory`.

##### ArgumentQuantity
```c#
int ArgumentQuantity { get; }
```
This returns the quantity of arguments. in the above example, this would be `3`.

##### GetArgument
```c#
string GetArgument(int index);
```
You can retreive your arguments by index. These will be ordered as they appear in the raw command. The following table represents what the example would give you:
```c#
GetArgument(0) => "insert";
GetArgument(1) => "13324678";
GetArgument(2) => "player";
```

##### GetFlag
```c#
string GetFlag(char flagName);
```
Flags differ from arguments as in most cases simply checking if the flag is present is all you need. This does support specific assignment of flags, as well. Given the above example, the following table describes what you have access to:
```c#
GetFlag('s') => "5";
GetFlag('v') => "true"; //This is a string, not a boolean
```

##### Spill Command
By default there is a command called `spill`. If you feed it a command, it will show you everything the arguments object will provide. For example:
`spill inventory insert 13324678 -s=5 player -v`
```
Text Entered: inventory insert 13324678 -s=5 player -v
Command: inventory
Arguments: 3
    0: insert
    1: 13324678
    2: player
Flags: 2
    s: 5
    v: true
```
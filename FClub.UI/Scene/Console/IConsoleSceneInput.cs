using System;

namespace FClub.UI.Scene.Console
{
	public interface IConsoleSceneInput : ISceneInput
	{
		ConsoleKeyInfo ConsoleKeyInfo { get; set; }
		IMenuComponent<IConsoleSceneInput> Target { get; set; }
	}
}

using System;

namespace FClub.UI.Scene.Console
{
	public class ConsoleSceneInput : IConsoleSceneInput
	{
		public ConsoleKeyInfo ConsoleKeyInfo { get; set; }
		public IMenuComponent<IConsoleSceneInput> Target { get; set; }
	}
}

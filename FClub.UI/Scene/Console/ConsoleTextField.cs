using System;
using System.Text.RegularExpressions;

namespace FClub.UI.Scene.Console
{
	public class ConsoleTextField : ConsoleBaseMenuComponent
	{
		public string Text { get; private set; }

		protected override void OnHandleInput(IConsoleSceneInput input)
		{
			switch (input.ConsoleKeyInfo.Key)
			{
				case ConsoleKey.Backspace: Backspace(); break;
				default: base.OnHandleInput(input); break;
			}
		}

		protected override void OnCharEntered(char character)
		{
			if (IsFocused &&
				Regex.IsMatch(character.ToString(), "[a-zA-Z0-9 ]"))
			{
				HighlightColors();
				Text += character;
				System.Console.Write(character);
				StdColors();
			}
		}

		private void Backspace()
		{
			if (Text.Length == 0)
			{
				return;
			}

			StdColors();
			System.Console.CursorLeft--;
			System.Console.Write(' ');
			System.Console.CursorLeft--;
			Text = Text[0..^1];
		}
	}
}

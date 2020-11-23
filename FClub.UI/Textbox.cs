using System;
using System.Text.RegularExpressions;

namespace FClub.UI
{
	public class Textbox : BaseMenuComponent
	{
		private int m_x = -1, m_y = -1;
		private string m_text = string.Empty;

		protected override void OnRender()
		{
			if (m_x == -1)
			{
				m_x = Console.CursorLeft;
				m_y = Console.CursorTop;
			}

			base.OnRender();
		}

		public string Text => m_text;

		protected override void OnEvent(IMenuComponentEvent menuComponentEvent)
		{
			if (!Focus)
			{
				return;
			}

			if (Regex.IsMatch(menuComponentEvent.KeyInfo.KeyChar.ToString(), "[a-zA-Z0-9 ]"))
			{
				WriteChar(menuComponentEvent.KeyInfo.KeyChar);
			}
			else if (m_text.Length > 0 && menuComponentEvent.KeyInfo.Key == ConsoleKey.Backspace)
			{
				RemoveChar();
			}
		}

		private void WriteChar(char c)
		{
			Console.CursorTop = m_y;
			Console.CursorLeft = m_x + m_text.Length;

			m_text += c;
			Console.Write(c);
		}

		private void RemoveChar()
		{
			Console.CursorTop = m_y;
			Console.CursorLeft = m_x + m_text.Length - 1;
			Console.Write(' ');
			Console.CursorLeft = m_x + m_text.Length - 1;
			m_text = m_text[0..^1];

		}
	}
}

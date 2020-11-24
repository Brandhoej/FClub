using System;

namespace FClub.UI.Scene.Console
{
	public class ConsoleLabel : ConsoleBaseMenuComponent
	{
		private readonly string m_text;
		private readonly bool m_endLine;

		public ConsoleLabel()
			: this(string.Empty)
		{ }

		public ConsoleLabel(object obj, bool endLine = true)
			: this(obj?.ToString(), endLine)
		{ }

		public ConsoleLabel(string text, bool endLine = true)
		{
			m_text = text;
			m_endLine = endLine;
		}

		protected override void OnRender()
		{
			System.Console.Write(m_text + (m_endLine ? Environment.NewLine : string.Empty));
		}
	}
}

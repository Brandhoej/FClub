using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace FClub.Controller.Command.Parser
{
	internal interface IStregsystemCommandParser : ICollection<StregsystemCommand>
	{
		StregsystemCommand DefaultCommand { get; }
		StregsystemCommand Parse(string input);
	}

	internal class StregsystemCommandParser : List<StregsystemCommand>, IStregsystemCommandParser
	{
		private readonly StregsystemCommand m_stregsystemCommand;

		public StregsystemCommandParser()
			: this(default)
		{ }

		public StregsystemCommandParser(StregsystemCommandStatement stregsystemCommandStatement)
		{
			m_stregsystemCommand = new StregsystemCommand(stregsystemCommandStatement);
		}

		public StregsystemCommand DefaultCommand => m_stregsystemCommand;

		public StregsystemCommand Parse(string input)
		{
			return Find(curr => curr.Match(input)) ?? m_stregsystemCommand;
		}
	}
}

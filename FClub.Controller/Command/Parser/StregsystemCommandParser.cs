using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace FClub.Controller.Command.Parser
{
	internal interface IStregsystemCommandParser : ICollection<StregsystemCommand>
	{
		StregsystemCommand Parse(string input);
	}

	internal class StregsystemCommandParser : List<StregsystemCommand>, IStregsystemCommandParser
	{
		public StregsystemCommand Parse(string input)
		{
			return Find(curr => curr.Match(input));
		}
	}
}

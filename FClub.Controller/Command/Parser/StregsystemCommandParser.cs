using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Collections;

namespace FClub.Controller.Command.Parser
{
	internal interface IStregsystemCommandParser
	{
		void Add(string endpoint, string name);
		StregsystemCommand Parse(string name, string input);
		IStregsystemCommandResult Run(object thisRef, string name, string input);
	}

	internal class StregsystemCommandParser : IStregsystemCommandParser
	{
		private readonly Type m_controller;
		private ICollection<StregsystemCommand> m_commands;

		public StregsystemCommandParser(Type Controller)
		{
			m_controller = Controller;
			m_commands = new HashSet<StregsystemCommand>();
		}

		public void Add(string endpoint, string name)
		{
			m_commands.Add(new StregsystemCommand(name, m_controller.GetMethod(endpoint)));
		}

		public StregsystemCommand Parse(string name, string input)
		{
			try
			{
				return m_commands.First(curr => curr.Match(name, input));
			}
			catch
			{
				return default;
			}
		}

		public IStregsystemCommandResult Run(object thisRef, string name, string input)
		{
			StregsystemCommand cmd = Parse(name, input);
			return cmd != null ? cmd.Run(thisRef, input) : default;
		}
	}
}

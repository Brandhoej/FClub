using FClub.Core;

namespace FClub.Controller.Command.Parser
{
	internal interface IStregsystemCommandParser
	{
		void Add(string endpoint, string name);
		StregsystemCommand Parse(string name, string input);
		IStregsystemCommandResult Run(object thisRef, string name, string input);
	}
}

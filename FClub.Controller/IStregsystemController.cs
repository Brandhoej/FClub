using FClub.Controller.Command;
using FClub.Core;

namespace FClub.Controller
{
	public interface IStregsystemController
	{
		IStregsystemCommandResult Execute(string name, string input = "");
	}
}

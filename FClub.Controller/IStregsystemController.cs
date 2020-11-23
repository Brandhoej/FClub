using FClub.Controller.Command;

namespace FClub.Controller
{
	public interface IStregsystemController
	{
		IStregsystemCommandResult Execute(string name, string input = "");
	}
}

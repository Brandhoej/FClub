using FClub.Controller.Command;

namespace FClub.Controller
{
	public class Ok : IStregsystemCommandResult
	{
		public override string ToString()
		{
			return "OK";
		}
	}
}

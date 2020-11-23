using FClub.Controller.Command;

namespace FClub.Controller
{
	public class Error : IStregsystemCommandResult
	{
		public override string ToString()
		{
			return "Error";
		}
	}
}

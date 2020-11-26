using FClub.Core;

namespace FClub.Core
{
	public class Ok : IStregsystemCommandResult
	{

		public override string ToString()
		{
			return "OK";
		}

		public int Code => 200;
	}
}

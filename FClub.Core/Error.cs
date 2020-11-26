namespace FClub.Core
{
	public class Error : IStregsystemCommandResult
	{
		public Error()
			: this(string.Empty)
		{ }

		public Error(string message)
		{
			Message = message;
		}

		public string Message { get; }

		public int Code => 400;

		public override string ToString()
		{
			return Message;
		}
	}
}

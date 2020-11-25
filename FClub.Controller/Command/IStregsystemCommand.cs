namespace FClub.Controller.Command
{
	internal interface IStregsystemCommand
	{
		IStregsystemCommandResult Run(object thisRef, string input);
		IStregsystemCommandResult Run(object thisRef, object[] parameters);

		bool Match(string name, string input);
		object[] GetParamsFromInput(string input);
	}
}

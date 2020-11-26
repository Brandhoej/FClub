using FClub.Model;
using System.Collections.Generic;

namespace FClub.DAL.IO
{
	public interface IUsersReader
	{
		string Separator { get; }
		string Path { get; }

		IEnumerable<User> ReadUsers();
		User CreateUserFromLine(string line);
	}
}

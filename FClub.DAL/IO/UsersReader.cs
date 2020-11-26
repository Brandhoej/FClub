using FClub.Model;
using System;
using System.Reflection;
using System.Text;

namespace FClub.DAL.IO
{
	public class UsersReader : DelimitedDocumentDatabase<User>
	{
		public UsersReader(string path, string separator)
			: base(path, separator)
		{ }

		public override User Construct(string[] columns)
		{
			if (columns.Length != 6)
			{
				throw new ArgumentException($"Must have 6 columns, but has {columns.Length}", nameof(columns));
			}

			try
			{
				int _id = int.Parse(columns[0]);
				string _firstName = columns[1];
				string _lastName = columns[2];
				string _username = columns[3];
				decimal _balance = decimal.Parse(columns[4]);
				string _email = columns[5];
				return new User(_id, _firstName, _lastName, _username, _email, _balance);
			}
			catch
			{
				return default;
			}
		}
	}
}

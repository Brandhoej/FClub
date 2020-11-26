using FClub.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FClub.DAL.IO
{
	public class UsersReader : IUsersReader
	{
		public UsersReader(string separator, string path)
		{
			Path = path ?? throw new ArgumentNullException(nameof(path), "Path cannot be null");
			Separator = separator ?? throw new ArgumentNullException(nameof(separator), "Seperator cannot be null");
		}

		public string Separator { get; }
		public string Path { get; }

		public IEnumerable<User> ReadUsers()
		{
			ICollection<User> _users = new List<User>();
			using StreamReader _reader = new StreamReader(Path);
			string header = _reader.ReadLine();
			string _line;
			while (!string.IsNullOrEmpty(_line = _reader.ReadLine()))
			{
				User _user = CreateUserFromLine(_line) ?? throw new Exception("User cannot be created from line");
				_users.Add(_user);
			}
			return _users;
		}

		public User CreateUserFromLine(string line)
		{
			string[] _split = line.Split(Separator, StringSplitOptions.RemoveEmptyEntries);
			if (_split.Length != 6)
			{
				throw new ArgumentException("Must have 5 separations", nameof(line));
			}

			int _id = int.Parse(_split[0]);
			string _firstName = _split[1];
			string _lastName = _split[2];
			string _username = _split[3];
			decimal _balance = decimal.Parse(_split[4]);
			string _email = _split[5];

			try
			{
				return new User(_id, _firstName, _lastName, _username, _email, _balance);
			}
			catch
			{
				return default;
			}
		}
	}
}

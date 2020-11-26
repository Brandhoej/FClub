using System;
using System.Collections.Generic;
using System.IO;

namespace FClub.DAL.IO
{
	public abstract class DelimitedDocumentDatabase<T> : IReadonlyDelimitedDocumentDatabase<T>
	{
		public DelimitedDocumentDatabase(string path, string separator)
		{
			Path = path ?? throw new ArgumentNullException(nameof(path));
			Separator = separator ?? throw new ArgumentNullException(nameof(separator));
		}

		public string Separator { get; }
		public string Path { get; }

		public IEnumerable<T> ReadAll()
		{
			ICollection<T> _values = new List<T>();
			using StreamReader _reader = new StreamReader(Path);

			// Assume the first line is just header info
			_reader.ReadLine();

			string _line;
			while (!string.IsNullOrEmpty(_line = _reader.ReadLine()))
			{
				_values.Add(Construct(_line) ?? throw new Exception("User cannot be created from line"));
			}

			return _values;
		}

		public T Construct(string line)
		{
			return Construct(line.Split(Separator, StringSplitOptions.RemoveEmptyEntries));
		}

		public abstract T Construct(string[] columns);
	}
}

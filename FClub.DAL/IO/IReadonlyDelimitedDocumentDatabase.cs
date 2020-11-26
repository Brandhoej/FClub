using System.Collections.Generic;

namespace FClub.DAL.IO
{
	public interface IReadonlyDelimitedDocumentDatabase<T>
	{
		string Separator { get; }
		string Path { get; }

		public IEnumerable<T> ReadAll();
		public T Construct(string line);
		public T Construct(string[] columns);
	}
}

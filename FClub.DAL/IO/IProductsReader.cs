using FClub.Model;
using System.Collections.Generic;

namespace FClub.DAL.IO
{
	public interface IProductsReader
	{
		string Separator { get; }
		string Path { get; }

		IEnumerable<Product> ReadProducts();
		Product CreateProductFromLine(string line);
	}
}

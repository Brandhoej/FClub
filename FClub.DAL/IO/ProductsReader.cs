using FClub.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace FClub.DAL.IO
{
	public interface IProductsReader
	{
		string Separator { get; }
		string Path { get; }

		IEnumerable<Product> ReadProducts();
		Product CreateProductFromLine(string line);
	}

	public class ProductsReader : IProductsReader
	{
		public ProductsReader(string separator, string path)
		{
			Path = path;
			Separator = separator;
		}

		public string Separator { get; }
		public string Path { get; }

		public IEnumerable<Product> ReadProducts()
		{
			ICollection<Product> _products = new List<Product>();
			using StreamReader _reader = new StreamReader(Path);
			string header = _reader.ReadLine();
			string _line;
			while (!string.IsNullOrEmpty(_line = _reader.ReadLine()))
			{
				Product _product = CreateProductFromLine(_line) ?? throw new Exception("Product cannot be created from line");
				_products.Add(_product);
			}
			return _products;
		}

		public Product CreateProductFromLine(string line)
		{
			const bool _canBeBoughtOnCredit = false;
			string[] _split = line.Split(Separator, StringSplitOptions.RemoveEmptyEntries);
			int _id = int.Parse(_split[0]);
			string _name = _split[1].Substring(1, _split[1].Length - 2);
			decimal _price = decimal.Parse(_split[2]);
			bool _active = _split[3] == "1" ? true : false;
			DateTime _deactiveDate = default;
			if (_split.Length > 4)
			{
				// 2008-12-06 12:00:00
				_deactiveDate = DateTime.ParseExact(_split[4].Substring(1, _split[4].Length - 2), "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
			}

			Console.WriteLine(_split.Length);

			try
			{
				if (_split.Length == 4)
				{
					return new Product(_id, _name, _price, _active, _canBeBoughtOnCredit);
				}
				else if (_split.Length == 5)
				{
					return new SeasonalProduct(_id, _name, _price, _canBeBoughtOnCredit, default, _deactiveDate);
				}
				else
				{
					return default;
				}
			}
			catch
			{
				throw;
			}
		}
	}
}

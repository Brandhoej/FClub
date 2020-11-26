using FClub.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace FClub.DAL.IO
{
	public class ProductsReader : DelimitedDocumentDatabase<Product>
	{
		public ProductsReader(string path, string separator)
			: base(path, separator)
		{ }

		public override Product Construct(string[] columns)
		{
			const bool _canBeBoughtOnCredit = false;

			if (columns.Length != 4 &&
				columns.Length != 5)
			{
				throw new ArgumentException($"Must have 4 or 5 columns, but has {columns.Length}", nameof(columns));
			}

			try
			{
				int _id = int.Parse(columns[0]);
				string _name = columns[1];
				decimal _price = decimal.Parse(columns[2]) / 100.0m;
				bool _active = columns[3] == "1";
				DateTime _deactiveDate = default;
				if (columns.Length > 4)
				{
					_deactiveDate = DateTime.ParseExact(columns[4], "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
				}

				if (columns.Length == 4)
				{
					return new Product(_id, _name, _price, _active, _canBeBoughtOnCredit);
				}
				if (columns.Length == 5)
				{
					return new SeasonalProduct(_id, _name, _price, _canBeBoughtOnCredit, default, _deactiveDate);
				}
				return default;
			}
			catch
			{
				return default;
			}
		}
	}
}

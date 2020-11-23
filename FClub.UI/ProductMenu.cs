using FClub.Model;
using System;
using System.Collections.Generic;

namespace FClub.UI
{
	public class ProductMenu : BaseMenuComponent
	{
		public ProductMenu(IEnumerable<Product> products)
		{
			AddProductLables(products);
		}

		private void AddProductLables(IEnumerable<Product> products)
		{
			int[] columnWidths = new int[3]
			{
				2, 4, 5
			};

			foreach (Product product in products)
			{
				columnWidths[0] = Math.Max(product.Id.ToString().Length, columnWidths[0]);
				columnWidths[1] = Math.Max(product.Name.ToString().Length, columnWidths[1]);
				columnWidths[2] = Math.Max(product.Price.ToString().Length, columnWidths[2]);
			}

			string format = "{0,-" + columnWidths[0] + "}   {1,-" + columnWidths[1] + "}   {2,-" + columnWidths[2] + "}";
			AddChild(new Label(string.Format(format, "Id", "Name", "Price")));
			foreach (Product product in products)
			{
				AddChild(new Label(string.Format(format, product.Id, product.Name, product.Price)));
			}
		}
	}
}

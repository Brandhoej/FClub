using FClub.Model;
using System;
using System.Collections.Generic;

namespace FClub.UI
{
	public class ButtonProductMenu : BaseMenuComponent
	{
		public IList<Button> buttons;
		public event Action<Product> OnProductClicked;

		public ButtonProductMenu(IEnumerable<Product> products, Action<Product> onProductClicked)
		{
			AddProductLables(products);
			OnProductClicked = onProductClicked;
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
			buttons = new List<Button>();
			foreach (Product product in products)
			{
				Button _button = new Button(string.Format(format, product.Id, product.Name, product.Price), () => OnProductClicked?.Invoke(product));
				buttons.Add(_button);
				AddChild(_button);
				AddChild(new Spacer());
			}

			for (int i = 1; i < buttons.Count - 1; i++)
			{
				buttons[i].Up = buttons[i - 1];
				buttons[i].Down = buttons[i + 1];

				buttons[i - 1].Down = buttons[i];
				buttons[i + 1].Up = buttons[i];
			}
		}
	}
}

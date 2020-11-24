using FClub.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace FClub.UI.Scene.Console.Prefabs
{
	public class ConsoleLabeledProductMenu : ConsoleBaseMenuComponent
	{
		private ConsoleButtonTextField m_consoleButtonTextField;
		private readonly IEnumerable<Product> m_products;
		private event Action<string> m_onQuickBuy;

		public ConsoleLabeledProductMenu(IEnumerable<Product> products, Action<string> onQuickBuy)
		{
			m_products = products;
			m_onQuickBuy += onQuickBuy;
			Build();
		}

		public override BaseMenuComponent<IConsoleSceneInput> GetFocusComponent()
		{
			return m_consoleButtonTextField;
		}

		private void Build()
		{
			AddChild(new ConsoleLabel("Hello!"));
			foreach (Product _product in m_products)
			{
				AddChild(new ConsoleLabel($"{_product.Id}   {_product.Name}"));
			}
			AddChild(new ConsoleLabel("Quickbuy>", false));
			AddChild(m_consoleButtonTextField = new ConsoleButtonTextField(m_onQuickBuy));
		}
	}
}
